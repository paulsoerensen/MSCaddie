using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Services;

namespace MSCaddie.Components.Pages
{
    public partial class ResultViewBase : ComponentBase
    {
        [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public ILogger<ResultViewBase> _logger { get; set; } = default!;
        [Inject] public IMatchService service { get; set; } = default!;

        public List<MatchResultDto> results { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            //matches = (await service.GetMatches()).ToList();
        }
    }
}
