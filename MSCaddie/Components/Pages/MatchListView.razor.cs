﻿using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;
using Radzen.Blazor;
using Radzen;
using MSCaddie.Components.Account;
using Microsoft.JSInterop;
using System.Text.Json;


namespace MSCaddie.Components.Pages;

public partial class MatchListViewBase : ComponentBase
{
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public ILogger<MatchListViewBase> _logger { get; set; } = default!;
    [Inject] public IMatchService matchSvc { get; set; } = default!;
    [Inject] public ICourseService courseSvc { get; set; } = default!;

    [Inject] public DialogService DialogService { get; set; }
    [Inject] public IJSRuntime JSRuntime { get; set; }

    public IEnumerable<MatchModel>? matches => allMatches?.Where(x => x.MatchDate > DateTime.Now.AddDays(-7) || allSeason == true);
    public IEnumerable<MatchModel> allMatches { get; set; } = default!;
    public string? Message { get; set; }

    protected bool allSeason { get; set; } = true;
    protected string matchFilter { get; set; } = "Alle matcher";
    protected MatchModel? matchModel;

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

    protected RadzenDataGrid<MatchModel> matchGrid;

    //IEnumerable<CourseModel> matchs;
    //IEnumerable<Customer> customers;
    //IEnumerable<Employee> employees;


    // Method to open the dialog and pass the matchId
    public async Task OpenMatch(int matchId)
    {
        await LoadStateAsync();

        await DialogService.OpenAsync<MatchDetailView>($"Match {matchId}",
               new Dictionary<string, object>() { { "MatchId", matchId } },
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



    protected async Task EditRow(MatchModel match)
    {
        if (!matchGrid.IsValid) return;

        matchModel = match;
        await matchGrid.EditRow(match);
    }

    protected void OnUpdateRow(MatchModel match)
    {
        matchSvc.UpsertMatch(match);
    }

    protected async Task SaveRow(MatchModel match)
    {
        await matchGrid.UpdateRow(match);
        matchModel = null;
    }

    protected void CancelEdit(MatchModel match)
    {
        matchGrid.CancelEditRow(match);
        matchModel = null;
    }

    //protected async Task DeleteRow(MatchModel match)
    //{
    //    Reset(match);

    //    if (match != null)
    //    {
    //        //dbContext.Remove<MatchModel>(match);
    //        //dbContext.SaveChanges();

    //        await matchGrid.Reload();
    //    }
    //    else
    //    {
    //        matchGrid.CancelEditRow(match);
    //        await matchGrid.Reload();
    //    }
    //}

    protected async Task InsertRow()
    {
        if (!matchGrid.IsValid) return;

        matchModel = new MatchModel();
        await matchGrid.InsertRow(matchModel);
    }

    protected async Task InsertAfterRow(MatchModel row)
    {
        if (!matchGrid.IsValid) return;

        matchModel = new MatchModel();
        await matchGrid.InsertAfterRow(matchModel, row);
    }

    protected void OnCreateRow(MatchModel match)
    {
        matchSvc.UpsertMatch(match);
        matchModel = null;
    }
}