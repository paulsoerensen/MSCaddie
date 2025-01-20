using MSCaddie.Shared.Models;
using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Services;

namespace MSCaddie.Components.Pages;

public partial class MemberListViewBase : ComponentBase, IDisposable
{
    [Inject] public IPlayerService service { get; set; } = default!;

    public IEnumerable<Player> members { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        members = await service.GetPlayers();
    }
    public void Dispose()
    {
        ; // LocationContainer.OnChange -= StateHasChanged;
    }
}
