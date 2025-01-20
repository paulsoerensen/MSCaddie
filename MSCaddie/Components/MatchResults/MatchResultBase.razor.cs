using AutoMapper;
using MSCaddie.Shared.Containers;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Extensions;
using MSCaddie.Shared.Services;
using Microsoft.AspNetCore.Components;
using MSCaddie.Components.Pages;
using BlazorBootstrap;

namespace MSCaddie.Components.MatchResults;

public class MatchResultBase : ComponentBase
{
    [CascadingParameter]
    protected MatchResultPageBase Parent { get; set; }
    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Inject] protected ToastService ToastService { get; set; } = default!;
    [Inject] public IMatchService? service { get; set; }
    [Inject] public ILogger<MatchResultBase> logger { get; set; } = default!;

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
}
