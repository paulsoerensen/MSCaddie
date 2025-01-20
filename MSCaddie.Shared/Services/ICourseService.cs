using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Club>> GetClubs();
        Task<bool> AddClub(Club model);
        Task<IEnumerable<CourseInfo>?> GetCourses();
        Task<IEnumerable<CourseInfo>?> GetCourses(int clubId);
    }
}