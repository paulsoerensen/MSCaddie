using MSCaddie.Shared.Dtos;
using MSCaddie.Client.Services;
using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Services;

namespace MSCaddie.Client.Pages
{
    public class MatchViewBase : ComponentBase, IDisposable
    {
        [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public ILogger<MatchViewBase> _logger { get; set; } = default!;
        [Inject] public IMatchService matchSvc { get; set; } = default!;
        [Inject] public ICourseService courseSvc { get; set; } = default!;

        public List<MatchDto> matches { get; set; } = default!;
        public List<ClubDto> clubs { get; set; } = default!;
        public List<CourseDto> courses { get; set; } = default!;
        public List<ListItem> matchForms { get; set; } = default!;


        public bool ShowBackdrop = false;

        protected override async Task OnInitializedAsync()
        {
            matches = (await matchSvc.GetMatches()).ToList();
            clubs = (await courseSvc.GetClubs()).ToList();
            //courses = (await courseSvc.GetCourses(644)).ToList();
            matchForms = (await matchSvc.GetMatchforms()).ToList();
        }

        public void Dispose()
        {
            ; // LocationContainer.OnChange -= StateHasChanged;
        }

        private MatchDto? dtoToInsert;
        private MatchDto? dtoToUpdate;

        protected async Task EditRow(MatchDto dto)
        {
            dtoToUpdate = dto;
            //await matchesGrid.EditRow(dto);
        }
        protected void OnCreateRow(MatchResultDto dto)
        {
            // For demo purposes only

            // For production
            //dbContext.SaveChanges();
        }

        protected void OnUpdateRow(MatchResultDto dto)
        {

            //matchSvc.UpsertMatch(MatchId, dto);

            dtoToUpdate = null;
        }

        protected async Task SaveRow(MatchDto dto)
        {
            //await matchSvc.UpsertResultMatch(MatchId, dto);
            //await matchesGrid.UpdateRow(dto);
        }

        protected void CancelEdit(MatchDto dto)
        {
            if (dto == dtoToInsert)
            {
                dtoToInsert = null;
            }

            dtoToUpdate = null;

            //matchesGrid.CancelEditRow(dto);

            //// For production
            //var orderEntry = dbContext.Entry(order);
            //if (orderEntry.State == EntityState.Modified)
            //{
            //    orderEntry.CurrentValues.SetValues(orderEntry.OriginalValues);
            //    orderEntry.State = EntityState.Unchanged;
            //}
        }

        protected async Task DeleteRow(MatchDto dto)
        {
            if (dto == dtoToInsert)
            {
                dtoToInsert = null;
            }

            if (dto == dtoToUpdate)
            {
                dtoToUpdate = null;
            }
            //await matchSvc.DeleteMatch(dto.MatchId.Value);

            //dto.Brutto = null;
        }


    }
}