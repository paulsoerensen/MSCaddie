using Microsoft.AspNetCore.Components;
using MSCaddie.Components.Pages;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;
using Radzen;
using Radzen.Blazor;

namespace MSCaddie.Components.MatchResults;

public class OtherResultsBase : MatchResultBase
{
    [Parameter]
    public MatchModel Match { get; set; }

    protected RadzenDataGrid<CompetitionResultModel> gameGrid;

    protected bool inserting = false;

    private MatchResultPageBase? TabControl { get; set; }
    [Inject] public ILogger<OtherResultsBase> logger { get; set; } = default!;
    [Inject] public ICompetitionService? competitionService { get; set; }

    protected IEnumerable<CompetitionResultModel> compResults;
    protected IEnumerable<ListEntryModel>? Competitions;
    protected string? Birdies;
    protected IEnumerable<MatchResultModel>? Results;
    protected MatchResultModel result { get; set; } = new MatchResultModel();

    protected override async Task OnInitializedAsync()
    {
        Competitions = await competitionService.GetCompetitions();
        logger.LogInformation($"LoadData: GetCompetitions,{Competitions.Count()} ");
        logger.LogInformation($"LoadData: GetMatchCompetitions,{compResults?.Count()} ");

        await base.OnInitializedAsync();
    }

    protected override async Task OnParametersSetAsync()
    {
        logger.LogInformation($"OtherResultsBase:OnParametersSetAsync");
        if (Match != null)
        {
            var birdieRes = await service.GetMatchBirdies(Match.MatchId);
            logger.LogInformation($"LoadData: GetMatchBirdies,{birdieRes?.Count()} ");
            List<string>? lst = birdieRes?.Select(i => i.BirdieString).ToList();
            Birdies = string.Join(",", lst);
        }
        await LoadData();
        await base.OnParametersSetAsync();
    }

    protected async Task LoadData()
    {
        if (Match != null)
        {
            compResults = await competitionService.GetMatchCompetitionResults(Match.MatchId);
        }
        inserting = false;
    }

    protected async Task OnDeleteResultClicked(int id)
    {
        logger.LogInformation($"OnDeleteResultClicked: {id}");
        await competitionService.DeleteCompetitionResult(id);
        logger.LogInformation($"OnDeleteResultClicked done");
        compResults = await competitionService.GetMatchCompetitionResults(result.MatchId);
    }

    protected async Task SaveResult(CompetitionResultModel model)
    {
        logger.LogInformation($"OnCompetitionSave: {model?.VgcNo}: {model.CompetitionText} ");
        var res = await competitionService.UpsertGetCompetitionResult(model);
        compResults = await competitionService.GetMatchCompetitionResults(result.MatchId);
        inserting = false;
    }

    protected async Task InsertRow()
    {
        if (inserting) return;

        var res = new CompetitionResultModel();
        await gameGrid.InsertRow(res);
        inserting = true;
    }

    protected async Task EditResult(CompetitionResultModel model)
    {
        await gameGrid.EditRow(model);
    }

    protected async Task CancelEdit(CompetitionResultModel model)
    {
        gameGrid.CancelEditRow(model);
    }

    protected async Task DeleteRow(CompetitionResultModel model)
    {
        int i = model.CompetitionResultId;
        if (i > 0)
        {
            await competitionService.DeleteCompetitionResult(i);
            await LoadData();
        }
    }

    private async Task AddCompetitionResult(string text, int vgcNo, string fullName)
    {
        if (!compResults.Any())
        {
            var comp = Competitions
                .Where(x => x.Value.Contains(text))
                .SingleOrDefault();
            if (comp != null)
            {
                CompetitionResultModel model = new()
                {
                    MatchId = Match.MatchId,
                    CompetitionId = comp.Key,
                    CompetitionText = comp.Value,
                    VgcNo = vgcNo,
                    Fullname = fullName
                };
                await SaveResult(model);
            }
        }
    }
}

