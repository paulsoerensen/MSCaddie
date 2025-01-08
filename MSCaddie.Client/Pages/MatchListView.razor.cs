using MSCaddie.Shared.Dtos;
using MSCaddie.Client.Services;
using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Services;

namespace MSCaddie.Client.Pages
{
    public class MatchListViewBase : ComponentBase, IDisposable
    {
        [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public ILogger<MatchListViewBase> _logger { get; set; } = default!;
        [Inject] public IMatchService matchSvc { get; set; } = default!;
        [Inject] public ICourseService courseSvc { get; set; } = default!;

        public IEnumerable<MatchDto>? matches => allMatches?.Where(x => x.MatchDate > DateTime.Now.AddDays(-7) || allSeason == true);
        public IEnumerable<MatchDto> allMatches { get; set; } = default!;
        public string? Message { get; set; }

        public bool allSeason { get; set; } = true;
        protected override async Task OnInitializedAsync()
        {
            allMatches = (await matchSvc.GetMatches()).ToList();
            //matchForms = (await matchSvc.GetMatchforms()).ToList();
            Message = $"* {allSeason}, {DateTime.Now.Second}";
        }

        protected void AllSeasonChanged(ChangeEventArgs e)
        {
            StateHasChanged();
            allSeason = Convert.ToBoolean(e.Value);
            Message = $"{allSeason}, {DateTime.Now.Second}";
        }

        public void Dispose()
        {
            ; // LocationContainer.OnChange -= StateHasChanged;
        }
    }
}