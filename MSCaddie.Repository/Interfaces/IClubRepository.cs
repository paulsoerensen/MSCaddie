using MSCaddie.Repository.Dtos;

namespace  MSCaddie.Repository.Interfaces;

public interface IClubRepository
{

    #region Club
    Task<ClubDto?> GetClub(int id);
    Task<IEnumerable<ClubDto>> GetClubs();
    Task<ClubDto> ClubUpsert(ClubDto model);
    #endregion

    #region Course
    Task<CourseDto?> GetCourse(int courseId);
    Task<IEnumerable<CourseDto>> GetCourses(int? clubId, int? courseId);
    Task<CourseDto> CourseUpsert(CourseDto model);
    #endregion

    #region Tee
    Task<ListEntryDto?> GetTee(int teeId);
    Task<IEnumerable<ListEntryDto>> GetTees();
    Task<ListEntryDto> TeeUpsert(ListEntryDto model);
    #endregion
}
