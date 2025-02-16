using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;
using Radzen.Blazor;


namespace MSCaddie.Components.Matchplay;

public partial class MatchplayTeamParBase : ComponentBase
{
    protected RadzenDataGrid<TeamParModel> teamsGrid;

    protected IEnumerable<PlayerModel>? players;
    protected IEnumerable<TeamParModel>? teams;

    protected int vgcNo1 = -1;
    protected int vgcNo2 = -1;
    [Inject] public IMatchplayService matchPlaySvc { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    protected int GetVgcNo(PlayerModel club) => club.VgcNo;
    protected string GetFullName(PlayerModel club) => club.Fullname;

    protected async Task LoadData()
    {
        players = await matchPlaySvc.GetTeamPartners();
        teams = await matchPlaySvc.GetMatchplayTeamPars();
    }

    public async Task CreateTeam()
    {
        if (vgcNo1 == vgcNo2 || vgcNo1 < 0 || vgcNo2 < 0)
            return;

        PlayerModel p1 = players.FirstOrDefault(player => player.VgcNo == vgcNo1);
        PlayerModel p2 = players.FirstOrDefault(player => player.VgcNo == vgcNo2);

        var dto = new TeamParModel()
        {
            VgcNo = vgcNo1,
            VgcNoPartner = vgcNo2,
            TeamName = $"{p1.Fullname}, {p2.Fullname}"
        };

        try
        {
            await matchPlaySvc.MatchplayTeamParUpsert(dto);
            await LoadData();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    protected async Task HandlePlayer1Selected(int selectedKey)
    {
        vgcNo1 = selectedKey;
    }
    protected async Task HandlePlayer2Selected(int selectedKey)
    {
        vgcNo2 = selectedKey;
    }

    protected async Task DeleteRow(TeamParModel model)
    {
        int i = model.TeamParId ?? 0;
        if (i > 0)
        {
            await matchPlaySvc.MatchplayTeamParDelete(i);
            await LoadData();
        }
    }
    protected async Task EditRow(TeamParModel model)
    {
        await teamsGrid.EditRow(model);
    }
    protected async Task CancelEdit(TeamParModel model)
    {
        teamsGrid.CancelEditRow(model);
        await LoadData();
    }

    protected async Task SaveRow(TeamParModel model)
    {
        try
        {
            await matchPlaySvc.MatchplayTeamParUpsert(model);
            await LoadData();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }    
    }
}