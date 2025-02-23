using AutoMapper;
using MSCaddie.Shared.Interfaces;
using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services;
public class CourseService : ICourseService
{
    private const string BaseAddress = "api/club";
    private const string BaseCourseAddress = "api/course";

    private readonly IClubRepository _repo;
    IMapper mapper;

    public CourseService(IClubRepository repo)
    {
        _repo = repo;
        mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ClubDto, ClubModel>().ReverseMap();
            cfg.CreateMap<CourseDto, CourseModel>().ReverseMap();
        })
        .CreateMapper();
    }
    public async Task<IEnumerable<ClubModel>> GetClubs()
    {
        var dtos = await _repo.GetClubs();
        return mapper.Map<IEnumerable<ClubModel>>(dtos);
        //return await _client.GetFromJsonAsync<IEnumerable<ClubDto>>(BaseAddress);
    }

    public async Task<bool> AddClub(ClubModel model)
    {
        var dto = mapper.Map<ClubDto>(model);
        dto =  await _repo.ClubUpsert(dto);
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

    public async Task<IEnumerable<CourseModel>?> GetCourses()
    {
        var dtos = await _repo.GetCourses(null, null);
        return mapper.Map<IEnumerable<CourseModel>>(dtos);
        //return await _client.GetFromJsonAsync<IEnumerable<CourseDto>>($"{BaseCourseAddress}");
    }

    public async Task<IEnumerable<CourseModel>?> GetCourses(int clubId)
    {
        var dtos = await _repo.GetCourses(clubId, null);
        return mapper.Map<IEnumerable<CourseModel>>(dtos);
        //return await _client.GetFromJsonAsync<IEnumerable<CourseDto>>($"{BaseAddress}/{clubId}/course");
    }
}

