using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;
using Radzen.Blazor;

namespace MSCaddie.Components.MatchResults;

public class ResultListBase : MatchResultBase
{
    [Parameter]
    public MatchModel Match { get; set; }
    protected IEnumerable<MatchResultModel>? results;

    [Inject] public IMatchService? service { get; set; }
    [Inject] public ICompetitionService? competitionService { get; set; }

    protected RadzenDataGrid<MatchResultModel> grid;
    protected int HcpGroup { get; set; } = 0;
    protected List<MatchResultModel>? filteredResults;
    protected string message = string.Empty;

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
        await service.MatchSettlement(Match.MatchId);
        results = await service.GetMatchResults(Match.MatchId);
        //results = results?.Where(x => x.Points != null).ToList();
        //MatchResultModel? res = results?
        //    .OrderBy(r => r.Rank)
        //    .SkipLast(1)
        //    .LastOrDefault();

        //if (res != null)
        //{
        //    CompetitionResultModel model = await competitionService.GetCompetitionResultModel("trøst");

        //    model.VgcNo = res.VgcNo; model.Fullname = res.Fullname;
        //    await competitionService.UpsertGetCompetitionResult(model);
        //}
        //res = results?
        //    .Where(x => x.InBirdies && x.Birdies > 0)
        //    .OrderBy(r => r.Birdies)
        //    .FirstOrDefault();

        //if (res != null)
        //{
        //    CompetitionResultModel model = await competitionService.GetCompetitionResultModel("birdie");

        //    model.VgcNo = res.VgcNo; model.Fullname = res.Fullname;
        //    await competitionService.UpsertGetCompetitionResult(model);
        //}

        StateHasChanged();
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
