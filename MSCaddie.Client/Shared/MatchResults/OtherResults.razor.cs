using MSCaddie.Client.Pages;
using MSCaddie.Shared.Containers;
using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Extensions;
using MSCaddie.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace MSCaddie.Client.Shared.MatchResults
{
    public class OtherResultsBase : MatchResultPage
    {
        [CascadingParameter(Name = "TbCtrl")]
        private MatchResultViewBase? TabControl { get; set; }
        [Inject] public ILogger<OtherResultsBase> logger { get; set; } = default!;
        [Inject] public ICompetitionService? competitionService { get; set; }

        protected IEnumerable<CompetitionResultDto> compResults;
        protected int competitionId;
        protected IEnumerable<ListItem>? competitions;
        protected bool _showModal = false;
        protected string? Birdies;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            try
            {
                competitions = await competitionService.GetCompetitions();
                logger.LogInformation($"LoadData: GetCompetitions,{competitions.Count()} ");
                compResults = await competitionService.GetMatchCompetitions(Match.MatchId);
                competitionId = competitions?.FirstOrDefault()?.KeyId ?? -1;
                logger.LogInformation($"LoadData: GetMatchCompetitions,{compResults?.Count()} ");

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
            CompetitionUpsertDto dto = new CompetitionUpsertDto
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
}
