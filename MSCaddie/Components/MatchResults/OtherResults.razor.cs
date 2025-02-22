using BlazorBootstrap;
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
    protected int competitionId;
    protected IEnumerable<ListEntryModel>? competitions;
    protected string? Birdies;
    protected IEnumerable<MatchResult>? Results;
    protected MatchResult result { get; set; } = new MatchResult();

    protected override async Task OnInitializedAsync()
    {
        competitions = await competitionService.GetCompetitions();
        logger.LogInformation($"LoadData: GetCompetitions,{competitions.Count()} ");
        logger.LogInformation($"LoadData: GetMatchCompetitions,{compResults?.Count()} ");

        await base.OnInitializedAsync();
    }

    protected override async Task OnParametersSetAsync()
    {
        logger.LogInformation($"OtherResultsBase:OnParametersSetAsync");
        if (Match != null)
        {
            var birdies = await service.GetMatchBirdies(Match.MatchId);
            logger.LogInformation($"LoadData: GetMatchBirdies,{birdies?.Count()} ");
            List<string> lst = birdies.Select(i => i.BirdieString).ToList();
            Birdies = string.Join(",", lst);
            Results = await service.MatchResultForRegistration(Match.MatchId);
            Results = Results?.Where(x => x.Points != null).ToList();
        }
        await LoadData();
        await base.OnParametersSetAsync();
    }

    protected async Task LoadData()
    {
        if (Match != null)
        {
            compResults = await competitionService.GetMatchCompetitionResults(Match.MatchId);
            competitionId = competitions?.FirstOrDefault()?.Key ?? -1;
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

        var res = new CompetitionResultModel() { MatchId = Match.MatchId };
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
}

