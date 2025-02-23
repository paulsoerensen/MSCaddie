using Azure.Core;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Models;
using Radzen;
using Radzen.Blazor;
namespace MSCaddie.Components.MatchResults;


public class PlayerResultBase : MatchResultBase
{
    [Parameter]
    public MatchModel Match { get; set; }
    [Inject] public ILogger<PlayerResultBase> logger { get; set; } = default!;
    //[Inject] protected MatchResultContainerList _matchResultContainerList { get; set; } = null!;
    protected RadzenDataGrid<MatchResultModel> resultsGrid;

    protected bool _showModal = false;
    protected string message = "";
    //protected Validations _validations { get; set; }
    protected Variant variant = Variant.Outlined;
    protected IEnumerable<MatchResultModel>? Results { get; set; }
    protected IEnumerable<MatchResultModel>? registredResults { get; set; }
    protected MatchResultModel result { get; set; } = new MatchResultModel();

    protected string Fullname;
    protected object selectedItem;

    protected override async Task OnInitializedAsync()
    {
        logger.LogInformation($"PlayerResult:OnInitializedAsync");
        await base.OnInitializedAsync();
    }

    protected async Task LoadData()
    {
        if (Match != null)
        {
            Results = await service.MatchResultForRegistration(Match.MatchId);
            registredResults = Results?.Where(x => x.Points != null).ToList();
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        logger.LogInformation($"PlayerResult:OnParametersSetAsync");
        await LoadData();
    }

    protected async Task<AutoCompleteDataProviderResult<MatchResultModel>> ResultDataProvider(AutoCompleteDataProviderRequest<MatchResultModel> request)
    {
        // Ensure that results is never null. If it's null, initialize as an empty collection.
        if (Results == null)
        {
            Results = Enumerable.Empty<MatchResultModel>(); // Initialize as an empty set if results is null
        }

        // If results is empty, fetch the data from the service
        if (!Results.Any())
        {
            Results = await service.MatchResultForRegistration(Match.MatchId);
        }

        // Filter results based on the request filter value
        var filteredResults = Results.Where(x => x.Fullname.Contains(request.Filter.Value, StringComparison.OrdinalIgnoreCase)).ToList();

        // Return the filtered results in an AutoCompleteDataProviderResult
        return await Task.FromResult(new AutoCompleteDataProviderResult<MatchResultModel>
        {
            Data = filteredResults, // return filtered results
            TotalCount = filteredResults.Count // return filtered count
        });
    }

    protected void OnAutoCompleteChanged(MatchResultModel res)
    {
        result = res;
    }

    protected async Task OnUpdate()
    {
        try
        {

            logger.LogInformation($"HandleValidSubmit {result}");
            bool b = await service.UpsertResultMatch(result); 
            if (b)
            {
                ToastService.Notify(new(ToastType.Success, $"Resultatet er opdateret."));
                await LoadData();
            }
            else
            {
                ToastService.Notify(new(ToastType.Danger, $"Ups - shit happened."));
            }
        }
        catch (Exception e)
        {
            message = e.Message;
            ToastService.Notify(new(ToastType.Danger, $"Error: {e.Message}."));
        }
    }

    protected async Task OnDelete()
    {
        try
        {
            if (result?.MatchResultId.HasValue == true)
            {
                await service!.DeleteResultMatch(result.MatchResultId.Value); ToastService.Notify(new(ToastType.Success, $"Employee details saved successfully."));
                ToastService.Notify(new(ToastType.Success, $"Resultatet er slettet."));
            }
            else
            {
                // Handle the case where MatchResultId is null, e.g., throw an exception
                ToastService.Notify(new(ToastType.Danger, "Intet resultat valg."));
            }
        }
        catch (Exception e)
        {
            ToastService.Notify(new(ToastType.Danger, $"Error: {e.Message}."));
        }
    }
}
