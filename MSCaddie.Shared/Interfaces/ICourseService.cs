using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<ClubModel>> GetClubs();
        Task<bool> AddClub(ClubModel model);
        Task<IEnumerable<CourseModel>?> GetCourses();
        Task<IEnumerable<CourseModel>?> GetCourses(int clubId);
    }
}