using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<ClubModel>> GetClubs();
        Task<bool> AddClub(ClubModel model);
        Task<IEnumerable<CourseInfo>?> GetCourses();
        Task<IEnumerable<CourseInfo>?> GetCourses(int clubId);
    }
}