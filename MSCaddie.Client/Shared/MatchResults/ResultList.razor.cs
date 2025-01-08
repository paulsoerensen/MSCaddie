using MSCaddie.Client.Pages;
using MSCaddie.Shared.Containers;
using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Extensions;
using MSCaddie.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Text.Json;

namespace MSCaddie.Client.Shared.MatchResults
{
    public class ResultListBase : MatchResultPage
    {
        //protected IEnumerable<MatchResultDto>? results;
        protected IEnumerable<MatchResultDto>? winnerResult;

        [Inject] public IMatchService? service { get; set; }

        protected int HcpGroup { get; set; } = 0;
        protected List<MatchResultDto>? filteredResults;
        protected string message = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (Parent == null)
                throw new ArgumentNullException(nameof(Parent), "TabPage must exist within a TabControl");
            Parent.SetResultListPage(this);
            await LoadResultsAsync();
            await base.OnInitializedAsync();
        }
        public async Task OnCompleteMatch()
        {
            var res = await service.MatchSettlement(Match.MatchId);
            await LoadResultsAsync();
        }

        protected async Task OnDisplayListChanged(int value)
        {
            logger.LogInformation("OnDisplayListChanged()");
            HcpGroup = value;
            FilterResult();
        }

        protected override void FilterResult()
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
            logger.LogInformation($"FilterResult, {filteredResults?.Count()}");
            StateHasChanged();
        }
    }
}
 