using MSCaddie.Shared.Dtos;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace MSCaddie.Shared.Services;
public class CourseService : ICourseService
{
    private const string BaseAddress = "api/club";
    private const string BaseCourseAddress = "api/course";

    private readonly HttpClient _client;

    public CourseService(HttpClient client)
    {
        _client = client;
    }
    public string Baseaddress => _client.BaseAddress?.ToString();

    public async Task<IEnumerable<ClubDto>> GetClubs()
    {
        return await _client.GetFromJsonAsync<IEnumerable<ClubDto>>(BaseAddress);
    }

    public async Task<bool> AddClub(ClubDto dto)
    {
        var response = await _client.PostAsJsonAsync<ClubDto>(BaseAddress, dto);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            string msg = await response.Content.ReadAsStringAsync();
            return false;;
        }
    }

    
    //public async Task<IEnumerable<ListItem>> GetClubNames()
    //{
    //    var res = await _client.GetFromJsonAsync<IEnumerable<ClubDto>>(BaseAddress);
    //    var dis = res.DistinctBy(x => x.ClubName);
    //    return dis.Select(x => new ListItem { KeyId = x.ClubName, KeyValue = x.ClubName });
    //}

    public async Task<IEnumerable<CourseDto>?> GetCourses()
    {
        return await _client.GetFromJsonAsync<IEnumerable<CourseDto>>($"{BaseCourseAddress}");
    }

    public async Task<IEnumerable<CourseDto>?> GetCourses(int clubId)
    {
        return await _client.GetFromJsonAsync<IEnumerable<CourseDto>>($"{BaseAddress}/{clubId}/course");
    }
}

