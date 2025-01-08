using MSCaddie.Shared.Dtos;
using MSCaddie.Services;
using Microsoft.AspNetCore.Components;
using MSCaddie.Client.Shared;
using MSCaddie.Shared.Services;
using Newtonsoft.Json.Linq;

namespace MSCaddie.Client.Pages
{
    public partial class ClubViewBase : ComponentBase, IDisposable 
    {
        #region Private Members
        private readonly CancellationTokenSource cts = new();

        #endregion

        [Inject] public ILogger<ClubViewBase> logger { get; set; } = default!;
        [Inject] public ICourseService service { get; set; } = default!;

        public int clubId { get; set; }
        public IEnumerable<ClubDto>? clubs;
        public IEnumerable<CourseDto>? courses = default!;
        public EventConsole? console;
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
                    logger.LogInformation($"Base: {service.Baseaddress}");
                    clubs = await service.GetClubs();
                
                    var club = clubs?.FirstOrDefault(x => x.ClubName.Equals("Vejle"));
                    await OnClubChange(club.ClubId);
                }
            }
            catch (Exception e)
            {
                message = e.ToString();
            }
        }

        protected async Task OnClubChange(int value)
        {
            if (value != null)
            {
                logger.LogInformation($"club # {clubId}");
                clubId = value;
                //int id = Int32.Parse(value);
                courses = (await service.GetCourses(clubId)).ToList();
                logger.LogInformation($"club# {clubId} - {courses.Count()}");
                StateHasChanged();
            }
        }

        
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
            ClubDto dto = new() { ClubName = NewClub };
            bool res = await service.AddClub(dto);
            if (res)
                ModalMessage = "Klub oprettet";
            else
                ModalMessage = "Fejl !!";
            await Task.Delay(2000);
            
            ModalDisplay = "none";
            ModalClass = "";
            ShowBackdrop = false;
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
}