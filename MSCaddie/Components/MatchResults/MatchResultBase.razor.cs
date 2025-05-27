using AutoMapper;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Interfaces;
using MSCaddie.Shared.Models;

namespace MSCaddie.Components.MatchResults;

public class MatchResultBase : ComponentBase
{
    [CascadingParameter]
    protected MatchResultPageBase Parent { get; set; }
    [Parameter]
    public RenderFragment ChildContent { get; set; }
    [Parameter]
    public MatchModel Match { get; set; }

    [Inject] protected ToastService ToastService { get; set; } = default!;
    [Inject] public IMatchService? matchService { get; set; }
    [Inject] public ILogger<MatchResultBase> logger { get; set; } = default!;

    protected IEnumerable<MatchResultModel>? matchPlayers;
    protected string Message = string.Empty;
    protected string TextMessage = string.Empty;
    protected string StatusClass = string.Empty;
    protected bool Saved;

    protected readonly IMapper mapper;

    public MatchResultBase()
    {
        mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<MatchModel, MatchModel>()
                .ReverseMap();
        }).CreateMapper();
    }

    protected override async Task OnInitializedAsync()
    {
        //_matchResultContainerList.OnChange += FetchOptions;
        logger.LogInformation($"MatchResultBase:OnInitializedAsync");
        await base.OnInitializedAsync();
    }
    protected override async Task OnParametersSetAsync()
    {
        logger.LogInformation($"OtherResultsBase:OnParametersSetAsync");
        if (Match != null && matchService != null)
        {
            var results = await matchService.GetMatchResults(Match.MatchId);
            matchPlayers = results?.OrderBy(x => x.Lastname);
        }
    }
}
