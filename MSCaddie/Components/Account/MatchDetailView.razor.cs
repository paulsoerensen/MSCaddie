using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;
using Radzen;

namespace MSCaddie.Components.Account;

public partial class MatchDetailViewBase : ComponentBase
{
    [Parameter]
    public int matchId { get; set; }

    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public ILogger<MatchDetailViewBase> _logger { get; set; } = default!;
    [Inject] public IMatchService matchSvc { get; set; } = default!;
    [Inject] public ICourseService courseSvc { get; set; } = default!;
    [Inject] public Radzen.DialogService dialogService { get; set; } = default!;

    public MatchModel? match { get; set; } = new MatchModel();
    public List<ClubModel> clubs { get; set; } = new List<ClubModel>();
    public List<CourseInfo> courses { get; set; } = new List<CourseInfo>();
    public List<CourseInfo> clubCourses { get; set; } = new List<CourseInfo>();
    public List<ListEntry> matchForms { get; set; } = new List<ListEntry>();
    public CourseInfo course { get; set; } = new CourseInfo();

    protected string Message = string.Empty;
    protected string StatusClass = string.Empty;
    protected bool Saved;
    protected int clubId = -1;


    public string? FilePath { get; set; }

    public bool ShowBackdrop = false;

    protected override async Task OnInitializedAsync()
    {
        matchForms = (await matchSvc.GetMatchforms()).ToList();
        clubs = (await courseSvc.GetClubs()).ToList();
        courses = (await courseSvc.GetCourses()).ToList();
        clubs = courses.GroupBy(x => x.ClubId)
            .Select(y => y.First()).Distinct()
            .Select(z => new ClubModel() { ClubId = z.ClubId, ClubName = z.ClubName })
            .ToList();

        if (matchId < 0)
        {
            match = new MatchModel();
            match.ClubId = clubs.Where(x => x.ClubName.StartsWith("Vejle")).FirstOrDefault().ClubId;
            match.MatchformId = matchForms.Where(x => x.Value.StartsWith("Stable")).FirstOrDefault().Key;
        }
        else
        {
            match = await matchSvc.GetMatch(matchId);
            course = courses.SingleOrDefault(x => x.CourseDetailId == match.CourseDetailId);
            match.ClubId = course.ClubId;
        }

        await OnClubChanged(match.ClubId);
    }

    protected async Task OnClubChanged(int i)
    {
        match.ClubId = i;
        clubCourses = courses.Where(x => x.ClubId == i).ToList();
        if (clubCourses.Any())
        {
            if (match.CourseDetailId > 0)
                course = clubCourses.SingleOrDefault(x => x.CourseDetailId == match.CourseDetailId);
            else
                course = clubCourses.FirstOrDefault();

            match.Par = course.Par;
            match.CourseDetailId = course.CourseDetailId;
        }
        StateHasChanged();
    }

    protected async Task HandleValidSubmit()
    {
        if (match.MatchId == 0) //new
        {
            match = await matchSvc.UpsertMatch(match);
            if (match != null)
            {
                StatusClass = "alert-success";
                Message = "Matchen blev oprettet.";
                Saved = true;
            }
            else
            {
                StatusClass = "alert-danger";
                Message = "Der gik noget galt. Prøv igen.";
                Saved = false;
            }
        }
        else
        {
            match = await matchSvc.UpsertMatch(match);
            StatusClass = "alert-success";
            Message = "Matchen er opdateret.";
            Saved = true;
        }
    }

    protected async Task HandleInValidSubmit()
    {
        Message = "Ups !!";
    }

    protected void NavigateToOverview()
    {
        NavigationManager.NavigateTo("/MatchListView");
    }

    public async Task OnInputFileChange(InputFileChangeEventArgs e)
    {
        using (StreamReader sr = new StreamReader(e.File.OpenReadStream()))
        {
            string? content = await sr.ReadToEndAsync();
            var res = await matchSvc.MatchRegistration(match.MatchId, content);
            Message = res;
        }
    }
}