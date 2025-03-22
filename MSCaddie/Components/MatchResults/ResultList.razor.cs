using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;
using Radzen;
using Radzen.Blazor;

namespace MSCaddie.Components.MatchResults;

public class ResultListBase : MatchResultBase
{
    [Parameter]
    public MatchModel Match { get; set; }
    protected IEnumerable<MatchResultModel>? results;

    [Inject] public IMatchService? service { get; set; }
    [Inject] public ICompetitionService? competitionService { get; set; }
    [Inject] public ILogger<ResultListBase> _logger { get; set; } = default!;

    protected RadzenDataGrid<MatchResultModel> grid;
    protected int HcpGroup { get; set; } = 0;
    protected List<MatchResultModel>? filteredResults;
    protected string message = string.Empty;
    protected int rank { get; set; } = -1;

    public enum ResultAction
    {
        All = 0,
        A = 1,
        B = 2
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
        _logger.LogInformation("OnParametersSetAsync");
        if (Match != null)
        {
            results = await service.GetMatchResults(Match.MatchId);
            logger.LogInformation($"OnParametersSet MatchId {Match.MatchId}");
            FilterResult();
        }
        await base.OnParametersSetAsync();
    }
    protected async Task OnFilterResult(ResultAction filter)
    {
        _logger.LogInformation("OnFilterResult");
        switch (filter)
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
        }
        StateHasChanged();
    }

    protected async Task OnSettleMatch()
    {
        _logger.LogInformation("OnSettleMatch");
        await service.MatchSettlement(Match.MatchId);
        OnParametersSetAsync();
    }

    protected void FilterResult()
    {
        _logger.LogInformation($"FilterResult, {DateTime.Now.Second} - HcpGroup: {HcpGroup}");
        if (Match.IsStrokePlay)
        {
            filteredResults = results?.Where(x => x.Points != null)
                .OrderBy(x => x.Netto)
                .ThenBy(x => x.HcpIndex).ToList();
        }
        else if (Match.IsHallington)
        {
            filteredResults = results?.Where(x => x.Points != null)
                .OrderByDescending(x => x.Hallington)
                .ThenBy(x => x.HcpIndex).ToList();
        }
        else
        {
            filteredResults = results?.Where(x => x.Points != null)
                .OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.HcpIndex).ToList();
        }
        filteredResults = filteredResults?.Where(x => (x.HcpGroup == HcpGroup) || (HcpGroup == 0)).ToList();
        rank = -1;
        StateHasChanged();
    }
    protected void OnRender(DataGridRenderEventArgs<MatchResultModel> args)
    {
        _logger.LogInformation("OnRender");
        if (args.FirstRender)
        {
            args.Grid.Groups.Add(new GroupDescriptor() { Title = "Placering", Property = "WinnerText", SortOrder = SortOrder.Descending });
            StateHasChanged();
        }
    }
    protected string DisplayRank(int i)
    {
        _logger.LogInformation($"DisplayRank({i}-{rank})");
        if (rank != i)
        {
            rank = rank < 1 ? rank+1 : i;
            if (rank < 1) return "1";
            return rank.ToString();
        }
        _logger.LogInformation($"DisplayRank - blank");
        return "";
    }
}
