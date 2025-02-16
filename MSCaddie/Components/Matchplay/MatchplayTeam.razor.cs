using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;
using Radzen.Blazor;


namespace MSCaddie.Components.Matchplay;

public partial class MatchplayTeamBase : ComponentBase
{
    protected RadzenDataGrid<TeamSingleModel> teamsGrid;

    protected IEnumerable<TeamSingleModel>? teams;
    [Inject] public IMatchplayService matchPlaySvc { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    protected async Task LoadData()
    {
        try
        {
            teams = await matchPlaySvc.GetMatchplayTeams();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    protected async Task DeleteRow(TeamSingleModel model)
    {
        int i = model.TeamSingleId ?? 0;
        if (i > 0)
        {
            await matchPlaySvc.MatchplayTeamDelete(i);
            await LoadData();
        }
    }
    protected async Task EditRow(TeamSingleModel model)
    {
        await teamsGrid.EditRow(model);
    }
    protected async Task CancelEdit(TeamSingleModel model)
    {
        teamsGrid.CancelEditRow(model);
        await LoadData();
    }

    protected async Task SaveRow(TeamSingleModel model)
    {
        try
        {
            await matchPlaySvc.MatchplayTeamUpsert(model);
            await LoadData();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }    
    }
}