using AutoMapper;
using MSCaddie.Client.Pages;
using MSCaddie.Shared.Containers;
using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Extensions;
using MSCaddie.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace MSCaddie.Client.Shared.MatchResults
{
    public class MatchResultPage : ComponentBase
    {
        [CascadingParameter]
        protected MatchResultView Parent { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public MatchDto? Match { get; set; }

        public MatchViewModel? matchViewModel { get; set; }

        //[Inject] protected INotificationService? NotificationService { get; set; }
        [Inject] public IMatchService? service { get; set; }
        [Inject] public ILogger<MatchResultPage> logger { get; set; } = default!;
        [Inject] protected SearchStateContainerList SearchState { get; set; } = null!;
        [Inject] protected MatchResultContainerList _matchResultContainerList { get; set; } = null!;


        protected MatchResultDto result = new();
        protected List<MatchResultDto>? results;

        protected string Message = string.Empty;
        protected string TextMessage = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected readonly IMapper mapper;

        public MatchResultPage()
        {
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MatchDto, MatchViewModel>()
                    .ReverseMap();
            }).CreateMapper();
        }

        protected override async Task OnInitializedAsync()
        {
            logger.LogInformation($"OnInitializedAsync base {Match}");
            matchViewModel = mapper.Map<MatchViewModel>(Match);
            logger.LogInformation($"OnInitializedAsync() {matchViewModel}");

            //results = (await service.MatchResultForRegistration(Match.MatchId)).ToList();
            _matchResultContainerList.MatchId = Match.MatchId;
            _matchResultContainerList.OnChange += FetchOptions;
            logger.LogInformation($"OnInitializedAsync base done");
            await base.OnInitializedAsync();
        }

        protected virtual async Task LoadResultsAsync()
        {
            logger.LogInformation($"LoadResultsAsync() start");
            results = (await service.MatchResultForRegistration(Match.MatchId)).ToList();
            logger.LogInformation($"LoadResultsAsync() got {results?.Any()}-{Match.MatchId}");
            FilterResult();
        }

        protected virtual void FilterResult()
        {; }

        public async Task Clicked()
        {
            logger.LogInformation("Clicked");
        }

        #region Search
        protected bool SearchFocus;
        protected bool SearchHover;

        protected string _searchTerm = "";

        protected string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                NavigateToPlayer();
            }
        }

        protected void SearchTermChanged(ChangeEventArgs changeEventArgs)
        {
            logger.LogInformation("SearchTermChanged called");
            Message = string.Empty;

            var updatedSearchTerm = changeEventArgs.Value?.ToString();

            if (updatedSearchTerm == null) return;
            logger.LogInformation($"SearchTermChanged {updatedSearchTerm}");

            SearchState.SetSearchTermAndNotify(updatedSearchTerm);
            SearchTerm = updatedSearchTerm;
        }
        protected void NavigateToPlayer()
        {
            logger.LogInformation($"NavigateToPlayer() - {_matchResultContainerList?.Content?.Any()}");
            var trimmedSearchTerm = SearchTerm.RemoveWhitespace();
            var foundMatchResult = _matchResultContainerList.Content?
                    .FirstOrDefault(result => result.Fullname != null
                        && result.Fullname.RemoveWhitespace().Equals(trimmedSearchTerm, StringComparison.OrdinalIgnoreCase));

            if (foundMatchResult == null) return;
            result = results?.Where(x => x.VgcNo == foundMatchResult.VgcNo).FirstOrDefault();

            logger.LogInformation($"NavigateToPlayer() - foundMatchResult {result?.VgcNo} - {result.Hcp}");
        }

        protected void FetchOptions()
        {
            logger.LogInformation("FetchOptions()");
            results = _matchResultContainerList.Content;
            StateHasChanged();
        }
        #endregion
    }
}
