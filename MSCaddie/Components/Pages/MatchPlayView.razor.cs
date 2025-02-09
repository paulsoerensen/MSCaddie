using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;
using Radzen.Blazor;


namespace MSCaddie.Components.Pages;

public partial class MatchPlayViewBase : ComponentBase
{
    protected RadzenDataGrid<PlayerForMatchPlayModel> playerGrid;

    public int vgcNo { get; set; }
    protected int GetPlayerNo(PlayerForMatchPlayModel p) => p.VgcNo;
    protected string GetPlayerName(PlayerForMatchPlayModel p) => p.Fullname;

    protected IEnumerable<PlayerForMatchPlayModel>? teams;
    protected IEnumerable<PlayerModel>? players;
    [Inject] public IMatchPlayService matchPlaySvc { get; set; } = default!;
    [Inject] public IPlayerService playerSvc { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        teams = await matchPlaySvc.GetPlayersForMatchPlay();
        players = await matchPlaySvc.GetPlayersForMatchPlayPar();
    }

    protected void OnSelectedPlayerChanged(int value)
    {
        vgcNo = value;
    }

    protected async Task DeleteRow(PlayerForMatchPlayModel model)
    {
        await matchPlaySvc.DeleteMatchPlayPar(model);
        teams = await matchPlaySvc.GetPlayersForMatchPlay();
    }
    protected async Task EditRow(PlayerForMatchPlayModel model)
    {
        await playerGrid.EditRow(model);
    }
    protected void CancelEdit(PlayerForMatchPlayModel model)
    {
        playerGrid.CancelEditRow(model);
    }

    protected async Task SaveRow(PlayerForMatchPlayModel model)
    {
        await matchPlaySvc.MatchPlayTeamUpsert(model);
        teams = await matchPlaySvc.GetPlayersForMatchPlay();
    }
}