using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;
using Radzen;

namespace MSCaddie.Components;

public partial class MatchDetailViewBase : ComponentBase
{
    [Parameter]
    public int matchId { get; set; }

    [Inject] public ILogger<MatchDetailViewBase> _logger { get; set; } = default!;
    [Inject] public IMatchService matchSvc { get; set; } = default!;
    [Inject] public ICourseService courseSvc { get; set; } = default!;
    [Inject] public DialogService dialogService { get; set; } = default!;

    public MatchModel? match { get; set; } = new MatchModel();
    public List<ClubModel> clubs { get; set; } = new List<ClubModel>();
    public List<CourseInfo> courses { get; set; } = new List<CourseInfo>();
    public List<CourseInfo> clubCourses { get; set; } = new List<CourseInfo>();
    public List<ListEntry> matchForms { get; set; } = new List<ListEntry>();
    public CourseInfo course { get; set; } = new CourseInfo();
    protected IEnumerable<string> matchTexts
        = ["Torsdagsmatch", "ÅbningsMatch", "Afslutningsmatch"];

    protected IEnumerable<string> sponsorNames
        = [ "MensSection", "AquaPri", "Autocentralen", "Danske Bank", "Den Lille Kok",
                            "DownUnderWines", "HC Auto", "HP Masking", "Isabella", "Modekompagniet",
                            "Motus Fit", "Nykredit", "Sidani Proshop", "Tøjeksperten", "Vis Performance" ];

    protected string Message = string.Empty;

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
            course = clubCourses.FirstOrDefault();

            match.Par = course?.Par ?? 72;
            match.CourseDetailId = course!.CourseDetailId;
        }
        StateHasChanged();
    }

    protected async Task Submit(MatchModel match)
    {
        try
        {
            Message = string.Empty;
            if (match.MatchId < 0) //new
            {
                match = await matchSvc.UpsertMatch(match);
            }
            else
            {
                match = await matchSvc.UpsertMatch(match);
            }
            dialogService.Close(true);
        }
        catch (Exception e)
        {
            Message = e.ToString();
        }
    }
}