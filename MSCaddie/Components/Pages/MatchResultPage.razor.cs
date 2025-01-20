using MSCaddie.Shared.Models;
using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Services;
using MSCaddie.Components.MatchResults;
using BlazorBootstrap;
using Radzen;
using MSCaddie.Shared.Extensions;

namespace MSCaddie.Components.Pages;

public partial class MatchResultPageBase : ComponentBase
{
    [Parameter]
    public RenderFragment ChildContent { get; set; }

    //[Inject] protected NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public ILogger<MatchResultPageBase> _logger { get; set; } = default!;
    [Inject] public IMatchService service { get; set; } = default!;


    protected TabPosition tabPosition = TabPosition.Top;

    public Tabs tabsRef = default!;
    public IEnumerable<MatchModel>? matches { get; set; } = default!;
    public int matchId { get; set; } = -1;
    public MatchModel? match { get; set; }

    //protected IEnumerable<MatchResult>? results;

    protected string _selectedTab = "Results";

    protected override async Task OnInitializedAsync()
    {
        matches = await service.GetMatches();
        match = matches.FirstOrDefault(r => r.MatchDate >= DateTime.Now.CustomDateTimeNow());
        await HandleMatchSelected(match.MatchId);
    }

    protected void OnChange(int index)
    {
        //console.Log($"Tab with index {index} was selected.");
    }

    protected async Task HandleMatchSelected(int selectedKey)
    {
        matchId = selectedKey;
        match = await service.GetMatch(matchId);
        _logger.LogInformation($"match: {match?.MatchDisplay}");
    }

    protected int GetMatchId(MatchModel match) => match.MatchId;
    protected string GetMatchText(MatchModel match) => match.MatchDisplay;

    protected void OnTabClick(TabEventArgs args)
    {
        ;// _selectedTab = tab;
    }

    protected async Task OnSelectedTabChanged(string name)
    {
        _selectedTab = name;
        await tabsRef.ShowTabByNameAsync(_selectedTab);

        //_logger.LogInformation($"OnSelectedTabChanged{name}");
        //return Task.CompletedTask;
    }
    internal void SetResultListPage(ResultListBase page)
    {
        resultListPage = page;
        StateHasChanged();
    }
    private ResultListBase resultListPage;
    protected void ResultListClicked()
    {
        //resultListPage.Clicked();
    }
}
