using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;
using Radzen.Blazor;

namespace MSCaddie.Components.MatchResults;

public class ResultListBase : MatchResultBase
{
    [Parameter]
    public MatchModel Match { get; set; }
    protected IEnumerable<MatchResult>? results;

    [Inject] public IMatchService? service { get; set; }

    protected RadzenDataGrid<MatchResult> grid;
    protected int HcpGroup { get; set; } = 0;
    protected List<MatchResult>? filteredResults;
    protected string message = string.Empty;

    public enum ResultAction
    {
        All = 0,
        A = 1,
        B = 2,
        X = 3
    }
    protected override async Task OnInitializedAsync()
    {
        if (Parent == null)
            throw new ArgumentNullException(nameof(Parent), "TabPage must exist within a TabControl");
        Parent.SetResultListPage(this);
        await base.OnInitializedAsync();
    }

    protected override async Task OnParametersSetAsync()
    {
        if (Match != null)
        {
            results = await service.GetMatchResults(Match.MatchId);
            logger.LogInformation($"OnParametersSet MatchId {Match.MatchId}");
            FilterResult();
        }
        await base.OnParametersSetAsync();
    }

    protected async Task OnButtonClick(ResultAction filter)
    {
        switch(filter)
        {
            case ResultAction.All:
                HcpGroup = 0;
                FilterResult();
                break;
            case ResultAction.A:
                HcpGroup = 1;
                FilterResult();
                break;
            case ResultAction.B:
                HcpGroup = 2;
                FilterResult();
                break;
            case ResultAction.X:
                var res = await service.MatchSettlement(Match.MatchId);
                results = await service.GetMatchResults(Match.MatchId);
                break;
        }
    }

    protected void FilterResult()
    {
        logger.LogInformation($"FilterResult, {DateTime.Now.Second} - HcpGroup: {HcpGroup}");
        if (Match.IsStrokePlay)
        {
            filteredResults = results?.Where(x => x.Points != null)
                .OrderByDescending(x => x.Dining)
                .ThenBy(x => x.Netto)
                .ThenBy(x => x.HcpIndex).ToList();
        }
        else if (Match.IsHallington)
        {
            filteredResults = results?.Where(x => x.Points != null)
                .OrderByDescending(x => x.Dining)
                .OrderByDescending(x => x.Hallington)
                .ThenBy(x => x.HcpIndex).ToList();
        }
        else
        {
            filteredResults = results?.Where(x => x.Points != null)
                .OrderByDescending(x => x.Dining)
                .ThenByDescending(x => x.Points)
                .ThenByDescending(x => x.HcpIndex).ToList();
        }
        filteredResults = filteredResults?.Where(x => (x.HcpGroup == HcpGroup) || (HcpGroup == 0)).ToList();
        StateHasChanged();
    }
}
