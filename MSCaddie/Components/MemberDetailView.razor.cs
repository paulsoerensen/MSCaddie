using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;
using Radzen;

namespace MSCaddie.Components;

public partial class MemberDetailViewBase : ComponentBase
{
    [Parameter]
    public int playerId { get; set; }

    [Inject] public ILogger<MatchDetailViewBase> _logger { get; set; } = default!;
    [Inject] public IPlayerService playerSvc { get; set; } = default!;
    [Inject] public DialogService dialogService { get; set; } = default!;

    public PlayerModel? player { get; set; } = new PlayerModel();
    public required IEnumerable<PlayerModel?> NonMembers { get; set; }
    protected string Fullname;

    protected string Message = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        if (playerId > 0)
        {
            player = await playerSvc.GetPlayer(playerId);
        }
        else
        {
            NonMembers = await playerSvc.GetNonMembers();
        }
    }
    protected async Task HandlePlayerSelected(PlayerModel model)
    {
        Message = string.Empty;
        player = model;
        Fullname = model?.Fullname ?? "";
    }

    protected async Task OnSubmit(PlayerModel player)
    {
        try
        {
            Message = string.Empty;
            await playerSvc.UpsertPlayer(player);
            player = null;
            Fullname = "";
            dialogService.Close(true);
        }
        catch (Exception e)
        {
            Message = e.ToString();
        }
    }

    protected async Task OnInvalidSubmit(FormInvalidSubmitEventArgs args)
    {
        ;
    }
}