using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;
using Radzen;

namespace MSCaddie.Components.Matchplay;

public partial class MatchplayResultBase : ComponentBase
{
    [Inject] public IMatchplayService matchPlaySvc { get; set; } = default!;

    IEnumerable<MatchTeamModel> teams;

    public TabPosition tabPosition = TabPosition.Top;

    protected override async Task OnInitializedAsync()
    {
        teams = await matchPlaySvc.GetMatchTeams(1);
        await base.OnInitializedAsync();
    }

    public void OnChange(int index)
    {
        ;
    }
}
