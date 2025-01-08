using MSCaddie.Shared.Dtos;
using MSCaddie.Client.Services;
using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Services;
using MSCaddie.Client.Shared.MatchResults;
using Microsoft.Extensions.Logging;
namespace MSCaddie.Client.Pages
{
    public class MatchResultViewBase : ComponentBase
    {
        [Parameter]
        public int matchId { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        public MatchDto? Match { get; set; }

        [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public ILogger<MatchResultViewBase> _logger { get; set; } = default!;
        [Inject] public IMatchService service { get; set; } = default!;

        protected string _selectedTab = "Results";

        protected override async Task OnInitializedAsync()
        {
            Match = await service.GetMatch(matchId);
        }

        protected Task OnSelectedTabChanged(string name)
        {
            _selectedTab = name;

            _logger.LogInformation($"OnSelectedTabChanged{name}");
            return Task.CompletedTask;
        }
        internal void SetResultListPage(ResultListBase page)
        {
            resultListPage = page;
            StateHasChanged();
        }
        private ResultListBase resultListPage;
        protected void ResultListClicked()
        {
            resultListPage.Clicked();
        }
    }
}
