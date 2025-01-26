using MSCaddie.Shared.Models;
using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Services;
using Microsoft.JSInterop;
using MSCaddie.Components.Account;
using Radzen.Blazor;
using Radzen;
using System.Text.Json;

namespace MSCaddie.Components.Pages;

public partial class MemberListViewBase : ComponentBase, IDisposable
{
    [Inject] public IPlayerService playerSvc { get; set; } = default!;
    [Inject] public DialogService DialogService { get; set; }
    [Inject] public IJSRuntime JSRuntime { get; set; }

    public IEnumerable<PlayerModel> players { get; set; } = default!;
    public PlayerModel playerModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        players = await playerSvc.GetPlayers();
    }

    protected RadzenDataGrid<PlayerModel> playerGrid;

    // Method to open the dialog and pass the matchId
    public async Task OpenPlayer(int playerId)
    {
        await LoadStateAsync();

        string txt;
        if (playerId > 0)
        {
            playerModel = await playerSvc.GetPlayer(playerId);
            txt = playerModel!.Fullname;
        }
        else
        {
            txt = "Nyt medlem";
        }

        var result = await DialogService.OpenAsync<MemberDetailView>(txt,
               new Dictionary<string, object>() { { "PlayerId", playerId } },
               new DialogOptions()
               {
                   Resizable = true,
                   Draggable = true,
                   Resize = OnResize,
                   Drag = OnDrag,
                   Width = Settings != null ? Settings.Width : "700px",
                   Height = Settings != null ? Settings.Height : "612px",
                   Left = Settings != null ? Settings.Left : null,
                   Top = Settings != null ? Settings.Top : null
               });

        if (result == null)
        {
            // CancelEditDetail(orderDetail);
        }
        else if (result)
        {
            players = await playerSvc.GetPlayers();
        }
        else
        {
            // CancelEditDetail(orderDetail);
        }

        await SaveStateAsync();
    }

    void OnDrag(System.Drawing.Point point)
    {
        JSRuntime.InvokeVoidAsync("eval", $"console.log('Dialog drag. Left:{point.X}, Top:{point.Y}')");

        if (Settings == null)
        {
            Settings = new DialogSettings();
        }

        Settings.Left = $"{point.X}px";
        Settings.Top = $"{point.Y}px";

        InvokeAsync(SaveStateAsync);
    }

    void OnResize(System.Drawing.Size size)
    {
        JSRuntime.InvokeVoidAsync("eval", $"console.log('Dialog resize. Width:{size.Width}, Height:{size.Height}')");

        if (Settings == null)
        {
            Settings = new DialogSettings();
        }

        Settings.Width = $"{size.Width}px";
        Settings.Height = $"{size.Height}px";

        InvokeAsync(SaveStateAsync);
    }

    DialogSettings _settings;
    public DialogSettings Settings
    {
        get
        {
            return _settings;
        }
        set
        {
            if (_settings != value)
            {
                _settings = value;
                InvokeAsync(SaveStateAsync);
            }
        }
    }

    private async Task LoadStateAsync()
    {
        await Task.CompletedTask;

        var result = await JSRuntime.InvokeAsync<string>("window.localStorage.getItem", "DialogSettings");
        if (!string.IsNullOrEmpty(result))
        {
            _settings = JsonSerializer.Deserialize<DialogSettings>(result);
        }
    }

    private async Task SaveStateAsync()
    {
        await Task.CompletedTask;

        await JSRuntime.InvokeVoidAsync("window.localStorage.setItem", "DialogSettings", JsonSerializer.Serialize<DialogSettings>(Settings));
    }

    public class DialogSettings
    {
        public string Left { get; set; }
        public string Top { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
    }

    public void Dispose()
    {
        ; // LocationContainer.OnChange -= StateHasChanged;
    }
}
