using Microsoft.AspNetCore.Components;
using MSCaddie.Components.Pages;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;

namespace MSCaddie.Components.MatchResults;

public class OtherResultsBase : MatchResultBase
{
    //[CascadingParameter(Name = "TbCtrl")]

    [Parameter]
    public MatchModel Match { get; set; }

    private MatchResultPageBase? TabControl { get; set; }
    [Inject] public ILogger<OtherResultsBase> logger { get; set; } = default!;
    [Inject] public ICompetitionService? competitionService { get; set; }

    protected IEnumerable<CompetitionResult> compResults;
    protected int competitionId;
    protected IEnumerable<ListEntry>? competitions;
    protected bool _showModal = false;
    protected string? Birdies;
    protected IEnumerable<MatchResult>? results;
    protected MatchResult? result;

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
            compResults = await competitionService.GetMatchCompetitions(Match.MatchId);
            competitionId = competitions?.FirstOrDefault()?.Key ?? -1;

            results = await service.GetMatchResults(Match.MatchId);
            var birdies = await service.GetMatchBirdies(Match.MatchId);
            logger.LogInformation($"LoadData: GetMatchBirdies,{birdies?.Count()} ");
            List<string> lst = birdies.Select(i => i.BirdieString).ToList();
            Birdies = string.Join(",", lst);
        }
        await base.OnParametersSetAsync();
    }

    private async Task LoadData()
    {
        try
        {
            competitions = await competitionService.GetCompetitions();
            logger.LogInformation($"LoadData: GetCompetitions,{competitions.Count()} ");
            compResults = await competitionService.GetMatchCompetitions(Match.MatchId);
            competitionId = competitions?.FirstOrDefault()?.Key ?? -1;
            logger.LogInformation($"LoadData: GetMatchCompetitions,{compResults?.Count()} ");
            results = await service.GetMatchResults(Match.MatchId);
            var birdies = await service.GetMatchBirdies(Match.MatchId);
            logger.LogInformation($"LoadData: GetMatchBirdies,{birdies?.Count()} ");
            List<string> lst = birdies.Select(i => i.BirdieString).ToList();
            Birdies = string.Join(",", lst);
        }
        catch (Exception e)
        {
            logger.LogError($"LoadData, {e}");
        }
    }
    protected async Task OnCompetitionSave()
    {
        logger.LogInformation($"OnCompetitionSave: {result?.VgcNo}: {competitionId} ");
        CompetitionUpsert dto = new CompetitionUpsert
        {
            MembershipId = result.MemberShipId,
            CompetitionId = competitionId,
            MatchId = result.MatchId,
            VgcNo = result.VgcNo
        };
        var res = await competitionService.UpsertGetCompetitionResult(dto);
        compResults = await competitionService.GetMatchCompetitions(result.MatchId);
    }

    protected async Task OnDeleteResultClicked(int id)
    {
        logger.LogInformation($"OnDeleteResultClicked: {id}");
        await competitionService.DeleteCompetitionResult(id);
        logger.LogInformation($"OnDeleteResultClicked done");
        compResults = await competitionService.GetMatchCompetitions(result.MatchId);
    }
}

