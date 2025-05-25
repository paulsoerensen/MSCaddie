using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;
using Radzen;
using Radzen.Blazor;

namespace MSCaddie.Components.MatchResults;

public class NearestPinBase : MatchResultBase
{
    public RadzenDataGrid<NearestPinResultModel> pinGrid;

    protected bool inserting = false;

    private MatchResultPageBase? TabControl { get; set; }
    [Inject] public ILogger<NearestPinBase> logger { get; set; } = default!;
    [Inject] public IMatchService? matchService { get; set; }
    [Inject] public DialogService DialogService { get; set; }
    [Inject] public IJSRuntime JSRuntime { get; set; }

    protected IEnumerable<NearestPinResultModel> results;
    protected IEnumerable<ListEntryModel>? par3s = new List<ListEntryModel>
    {
        new ListEntryModel { Key = 1, Value = "Parken3" },
        new ListEntryModel { Key = 2, Value = "Parken8" },
        new ListEntryModel { Key = 3, Value = "Sletten2" },
        new ListEntryModel { Key = 4, Value = "Sletten5" },
        new ListEntryModel { Key = 5, Value = "Sletten6" },
        new ListEntryModel { Key = 6, Value = "Skoven4" },
        new ListEntryModel { Key = 7, Value = "Skoven7" },
        new ListEntryModel { Key = 8, Value = "1.par3" },
        new ListEntryModel { Key = 9, Value = "2.par3" }
    };

    protected NearestPinResultModel result { get; set; } = new NearestPinResultModel();

    protected override async Task OnInitializedAsync()
    {
        results = Enumerable.Empty<NearestPinResultModel>();
        await base.OnInitializedAsync();
    }

    protected override async Task OnParametersSetAsync()
    {
        logger.LogInformation($"NearestPinBase:OnParametersSetAsync");
        await LoadData();
        await base.OnParametersSetAsync();
    }

    protected async Task LoadData()
    {
        if (Match != null)
        {
            results = await matchService.GetNearestPinResults(Match.MatchId);
        }
        inserting = false;
    }

    public async Task NewResult(int id)
    {
        string txt = "Nærmest flaget";
        //if (matchId > 0)
        //{
        //    matchModel = await matchSvc.GetMatch(matchId);
        //    txt = matchModel!.MatchText;
        //}
        //else
        //{
        //    txt = "Ny match";
        //}

        await DialogService.OpenAsync<NearestPinView>(txt,
               new Dictionary<string, object>() { { "MatchId", Match.MatchId } },
               new DialogOptions()
               {
                   Resizable = true,
                   Draggable = true,
                   //Resize = OnResize,
                   //Drag = OnDrag,
                   Width = "700px",
                   Height = "250px",
                   Left = null,
                   Top = null
               });

        //await SaveStateAsync();
    }

    protected async Task OnDeleteResultClicked(int id)
    {
        logger.LogInformation($"OnDeleteResultClicked: {id}");
        await matchService.DeleteNearestPinResult(id);
        await LoadData();
    }

    protected async Task SaveResult(NearestPinResultModel model)
    {
        logger.LogInformation($"SaveResult: {model?.VgcNo}: {model.PinName} ");
        model.MatchId = Match.MatchId;
        try
        {
            var res = await matchService.UpdateNearestPinResult(model);
            await pinGrid.UpdateRow(model);
            await LoadData();
        }
        catch (Exception e)
        {
            logger.LogError($"NearestPinBase:SaveResult");
        }
    }

    protected async Task InsertRow()
    {
        if (inserting) return;
        pinGrid.ShowEmptyMessage = false;
        var res = new NearestPinResultModel() { MatchId = Match.MatchId };
        await pinGrid.InsertRow(res);
        inserting = true;
    }

    protected async Task EditResult(NearestPinResultModel model)
    {
        await pinGrid.EditRow(model);
    }

    protected async Task CancelEdit(NearestPinResultModel model)
    {
        pinGrid.CancelEditRow(model);
    }

    protected async Task DeleteRow(NearestPinResultModel model)
    {
        int i = model.NearestPinId;
        if (i > 0)
        {
            await matchService.DeleteNearestPinResult(i);
            await LoadData();
        }
    }
    protected void OnCreateRow(NearestPinResultModel model)
    {
        ;
    }
    protected void OnUpdateRow(NearestPinResultModel model)
    {
        ;
    }
}
