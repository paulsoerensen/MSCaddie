using MSCaddie.Shared.Dtos;

namespace MSCaddie.Shared.Services
{
    public interface ICourseService
    {
        string Baseaddress { get; }

        Task<IEnumerable<ClubDto>> GetClubs();
        Task<bool> AddClub(ClubDto dto);
        Task<IEnumerable<CourseDto>?> GetCourses();
        Task<IEnumerable<CourseDto>?> GetCourses(int clubId);
    }
}