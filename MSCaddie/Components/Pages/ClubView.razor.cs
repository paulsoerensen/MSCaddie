using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace MSCaddie.Components.Pages;

public partial class ClubViewBase : ComponentBase, IDisposable 
{
    #region Private Members
    private readonly CancellationTokenSource cts = new();

    #endregion

    [Inject] public ILogger<ClubViewBase> logger { get; set; } = default!;
    [Inject] public ICourseService service { get; set; } = default!;

    public int clubId { get; set; } = -1;
    public IEnumerable<Club>? clubs;
    public IEnumerable<CourseInfo>? courses = default!;
    //public EventConsole? console;
    protected string? message;

    public string NewClub { get; set; }
    public string ModalMessage { get; set; }
    public string ModalDisplay = "none;";
    public string ModalClass = "";
    
    public bool ShowBackdrop = false;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            logger.LogInformation("OnInitializedAsync");
            if (service != null)
            {
                logger.LogInformation("_service != null");
                clubs = await service.GetClubs();
            
                var club = clubs?.FirstOrDefault(x => x.ClubName!.Equals("Vejle"));
                clubId = club!.ClubId;
                await HandleClubSelected(clubId);
            }
        }
        catch (Exception e)
        {
            message = e.ToString();
        }
    }
    protected async Task HandleClubSelected(int selectedKey)
    {
        clubId = selectedKey;
        courses = await service.GetCourses(clubId);
        logger.LogInformation($"club: {clubId} - #{courses.Count()}");
    }
    protected int GetClubId(Club club) => club.ClubId;
    protected string GetClubName(Club club) => club.ClubName;
    
    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                cts.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        // TODO: uncomment the following line if the finalizer is overridden above.
        // GC.SuppressFinalize(this);
    }
    #endregion

    public async Task OpenClub()
    {
        ModalDisplay = "block;";
        ModalClass = "Show";
        ShowBackdrop = true;
        StateHasChanged();
    }

    public async Task SaveClub()
    {
            try
            {
                Club model = new() { ClubName = NewClub };
                bool res = await service.AddClub(model);
                if (res)
                    ModalMessage = "Klub oprettet";
                else
                    ModalMessage = "Fejl !!";

                ModalDisplay = "none";
                ModalClass = "";
                ShowBackdrop = false;
                clubs = await service.GetClubs();
        }
        catch (Exception ex)
        {
            ModalMessage = ex.Message;
        }
        await Task.Delay(2000);

        StateHasChanged();
    }

    public async Task CancelClub()
    {
        ModalDisplay = "none";
        ModalClass = "";
        ShowBackdrop = false;
        StateHasChanged();
    }
}