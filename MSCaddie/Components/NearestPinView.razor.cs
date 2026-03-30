using Microsoft.AspNetCore.Components;
using MSCaddie.Repository.Interfaces;
using MSCaddie.Repository.Models;
using Radzen;

namespace MSCaddie.Components;

public partial class NearestPinViewBase : ComponentBase
{
    [Parameter]
    public int matchId { get; set; }

    [Inject] public ILogger<NearestPinViewBase> _logger { get; set; } = default!;
    [Inject] public IMatchService matchService { get; set; } = default!;
    [Inject] public IPlayerService playerSvc { get; set; } = default!;
    [Inject] public DialogService dialogService { get; set; } = default!;

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
    public required IEnumerable<PlayerModel?> players { get; set; }
    public required PlayerModel? player;
    protected string Fullname;
    protected string Message = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        players = await playerSvc.GetPlayers();
    }

    protected async Task HandlePlayerSelected(PlayerModel model)
    {
        Message = string.Empty;
        player = model;
        Fullname = model?.Fullname ?? "";
    }

    protected async Task SaveResult()
    {

        Message = string.Empty;
        result.Fullname = Fullname;
        result.MatchId = matchId;
        if (player != null)
            result.VgcNo = player.VgcNo;

        if (result.VgcNo == 0)
            return;
        if (result.MatchId == 0)
            return;
        if (result.DistanceInCM == 0)
            return;

        try
        {
            Message = string.Empty;
            await matchService.UpdateNearestPinResult(result);
            result = null;
            Fullname = "";
            dialogService.Close(true);
        }
        catch (Exception e)
        {
            Message = e.ToString();
        }
    }
}