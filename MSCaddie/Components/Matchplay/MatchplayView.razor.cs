using Microsoft.AspNetCore.Components;
using MSCaddie.Repository.Interfaces;
using MSCaddie.Repository.Models;
using Radzen.Blazor;


namespace MSCaddie.Components.Matchplay;

public partial class MatchplayViewBase : ComponentBase
{
    protected RadzenDataGrid<PlayerForMatchplayModel> playerGrid;

    public int vgcNo { get; set; }
    protected int GetPlayerNo(PlayerForMatchplayModel p) => p.VgcNo;
    protected string GetPlayerName(PlayerForMatchplayModel p) => p.Fullname;

    protected IEnumerable<PlayerForMatchplayModel>? teams;
    protected IEnumerable<PlayerModel>? players;
    [Inject] public IMatchplayService matchPlaySvc { get; set; } = default!;
    [Inject] public IPlayerService playerSvc { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        teams = await matchPlaySvc.GetPlayersForMatchplay();
        //players = await matchPlaySvc.GetPlayersForMatchplayPar();
    }

    protected void OnSelectedPlayerChanged(int value)
    {
        vgcNo = value;
    }

    protected async Task DeleteRow(PlayerForMatchplayModel model)
    {
        await matchPlaySvc.DeleteMatchplayPar(model);
        teams = await matchPlaySvc.GetPlayersForMatchplay();
        players = await matchPlaySvc.GetPlayersForMatchplayPar();
    }
    protected async Task EditRow(PlayerForMatchplayModel model)
    {
        await playerGrid.EditRow(model);
    }
    protected void CancelEdit(PlayerForMatchplayModel model)
    {
        playerGrid.CancelEditRow(model);
    }

    protected async Task SaveRow(PlayerForMatchplayModel model)
    {
        if (model.VgcNo == model.VgcNoPartner)
            return;
        //await matchPlaySvc.MatchplayTeamUpsert(model);
        teams = await matchPlaySvc.GetPlayersForMatchplay();
    }
}