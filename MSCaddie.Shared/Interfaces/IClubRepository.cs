using MSCaddie.Shared.Models;

namespace  MSCaddie.Shared.Interfaces;

public interface IClubRepository
{

    #region Club
    Task<ClubModel?> GetClub(int id);
    Task<IEnumerable<ClubModel>> GetClubs();
    Task<ClubModel> ClubUpsert(ClubModel model);
    #endregion

    #region Course
    Task<CourseInfo?> GetCourse(int courseId);
    Task<IEnumerable<CourseInfo>> GetCourses(int? clubId, int? courseId);
    Task<CourseInfo> CourseUpsert(CourseInfo model);
    #endregion

    #region Tee
    Task<ListEntry?> GetTee(int teeId);
    Task<IEnumerable<ListEntry>> GetTees();
    Task<ListEntry> TeeUpsert(ListEntry model);
    #endregion
}
