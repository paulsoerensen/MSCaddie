using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;

namespace MSCaddie.Components.Pages;

public partial class MatchListViewBase : ComponentBase, IDisposable
{
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public ILogger<MatchListViewBase> _logger { get; set; } = default!;
    [Inject] public IMatchService matchSvc { get; set; } = default!;
    [Inject] public ICourseService courseSvc { get; set; } = default!;

    public IEnumerable<MatchModel>? matches => allMatches?.Where(x => x.MatchDate > DateTime.Now.AddDays(-7) || allSeason == true);
    public IEnumerable<MatchModel> allMatches { get; set; } = default!;
    public string? Message { get; set; }

    public bool allSeason { get; set; } = true;
    public string matchFilter { get; set; } = "Alle matcher";
    protected override async Task OnInitializedAsync()
    {
        allMatches = (await matchSvc.GetMatches()).ToList();
        //matchForms = (await matchSvc.GetMatchforms()).ToList();
        Message = $"* {allSeason}, {DateTime.Now.Second}";
    }

    protected void ChangeMatchFilter()
    {
        allSeason = !allSeason;
        Message = $"{allSeason}, {DateTime.Now.Second}";
        matchFilter = allSeason ? "Alle matcher" : "Kun aktuelle matcher";
        StateHasChanged();
    }

    public void Dispose()
    {
        ; // LocationContainer.OnChange -= StateHasChanged;
    }
}