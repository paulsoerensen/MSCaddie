using MSCaddie.Shared.Dtos;
using MSCaddie.Client.Services;
using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Services;

namespace MSCaddie.Client.Pages
{
    public class MemberListViewBase : ComponentBase, IDisposable
    {
        [Inject] public IPlayerService service { get; set; } = default!;

        public List<PlayerDto> members { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            members = (await service.GetPlayers()).ToList();
        }
        public void Dispose()
        {
            ; // LocationContainer.OnChange -= StateHasChanged;
        }
    }

}
