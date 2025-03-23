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
    protected RadzenDataGrid<MatchResultModel> resultsGrid;

    protected bool _showModal = false;
    protected string message = "";
    //protected Validations _validations { get; set; }
    protected Variant variant = Variant.Outlined;
    protected IEnumerable<MatchResultModel>? Results { get; set; }
    protected IEnumerable<MatchResultModel>? registredResults { get; set; }
    protected MatchResultModel result { get; set; }

    protected string Fullname;
    protected object selectedItem;

    protected override async Task OnInitializedAsync()
    {
        logger.LogInformation($"PlayerResult:OnInitializedAsync");
        result = new MatchResultModel();
        await base.OnInitializedAsync();
    }

    protected async Task LoadData()
    {
        if (Match != null)
        {
            Results = await service.MatchResultForRegistration(Match.MatchId);
            registredResults = Results?.Where(x => x.Points != null)
                                        .OrderByDescending(x => x.MatchResultId).ToList();
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        logger.LogInformation($"PlayerResult:OnParametersSetAsync");
        await LoadData();
    }

    protected async Task<AutoCompleteDataProviderResult<MatchResultModel>> ResultDataProvider(AutoCompleteDataProviderRequest<MatchResultModel> request)
    {
        if (Results == null)
        {
            Results = Enumerable.Empty<MatchResultModel>(); // Initialize as an empty set if results is null
        }

        if (!Results.Any())
        {
            Results = await service.MatchResultForRegistration(Match.MatchId);
        }

        var filteredResults = Results.Where(x => x.Fullname.Contains(request.Filter.Value, StringComparison.OrdinalIgnoreCase)).ToList();
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
    protected async Task OnInvalidSubmit(FormInvalidSubmitEventArgs args)
    {
        //ToastService.Notify(new(ToastType.Danger, $"Error: {message}."));
    }

    protected async Task OnSubmit(MatchResultModel model)
    {
        try
        {
            logger.LogInformation($"HandleValidSubmit {result}");
            bool b = await service.UpsertResultMatch(result); 
            if (b)
            {
                ToastService.Notify(new(ToastType.Success, $"Resultatet er opdateret."));
                await LoadData();
                result = new MatchResultModel();
                Fullname = "";
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

    protected async Task OnEdit(MatchResultModel model)
    {
        result = model;
        Fullname = model.Fullname;
    }

    protected async Task OnDelete(MatchResultModel model)
    {
        try
        {
            if (model?.MatchResultId.HasValue == true)
            {
                await service!.DeleteResultMatch(model.MatchResultId.Value); 
                ToastService.Notify(new(ToastType.Success, $"Resultatet er slettet."));
                await LoadData();
            }
            else
            {
                // Handle the case where MatchResultId is null, e.g., throw an exception
                ToastService.Notify(new(ToastType.Danger, "Intet resultat valgt."));
            }
        }
        catch (Exception e)
        {
            ToastService.Notify(new(ToastType.Danger, $"Error: {e.Message}."));
        }
    }
}
