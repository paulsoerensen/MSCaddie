using MSCaddie.Shared.Interfaces;
using MSCaddie.Shared.Models;
using System.Net.Http.Json;

namespace MSCaddie.Shared.Services;
public class CourseService : ICourseService
{
    private const string BaseAddress = "api/club";
    private const string BaseCourseAddress = "api/course";

    private readonly IClubRepository _repo;

    public CourseService(IClubRepository repo)
    {
        _repo = repo;
    }
    public async Task<IEnumerable<ClubModel>> GetClubs()
    {
        return await _repo.GetClubs();
        //return await _client.GetFromJsonAsync<IEnumerable<ClubDto>>(BaseAddress);
    }

    public async Task<bool> AddClub(ClubModel model)
    {
        model =  await _repo.ClubUpsert(model);
        return model != null;
        //var response = await _client.PostAsJsonAsync<ClubDto>(BaseAddress, dto);
        //if (response.IsSuccessStatusCode)
        //{
        //    return true;
        //}
        //else
        //{
        //    string msg = await response.Content.ReadAsStringAsync();
        //    return false;;
        //}
    }

    
    //public async Task<IEnumerable<ListItem>> GetClubNames()
    //{
    //    var res = await _client.GetFromJsonAsync<IEnumerable<ClubDto>>(BaseAddress);
    //    var dis = res.DistinctBy(x => x.ClubName);
    //    return dis.Select(x => new ListItem { KeyId = x.ClubName, KeyValue = x.ClubName });
    //}

    public async Task<IEnumerable<CourseInfo>?> GetCourses()
    {
        return await _repo.GetCourses(null, null);
        //return await _client.GetFromJsonAsync<IEnumerable<CourseDto>>($"{BaseCourseAddress}");
    }

    public async Task<IEnumerable<CourseInfo>?> GetCourses(int clubId)
    {
        return await _repo.GetCourses(clubId, null);
        //return await _client.GetFromJsonAsync<IEnumerable<CourseDto>>($"{BaseAddress}/{clubId}/course");
    }
}

