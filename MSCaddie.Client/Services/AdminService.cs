using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using MSCaddie.Shared.Dtos;
using System.Collections.Generic;

namespace MSCaddie.Client.Services
{
    public interface IAdminService
    {
        //Task AddErrorMessage(string source, string text);
        //Task<EnvironmentModel?> GetEnvironment();
        //Task<IEnumerable<ErrorMessageData>> GetErrorMessages();
        Task<IEnumerable<MatchDto>> GetMatches();
        Task<List<ClubDto>> GetClubs();
        Task<List<CourseDto>> GetCourses(int clubId);

        //Task<int> UpsertGisStation(GridsiteModel site);
        //Task<LocationDto?> UpsertLocation(LocationDto location);
    }

    public class AdminService : ClientAPI, IAdminService
    {
        private readonly ILogger<AdminService> _log;

        public AdminService(HttpClient client, ILogger<AdminService> log)
            : base(client)
        {
            _log = log;
        }


        //#region log
        public async Task<IEnumerable<MatchDto>> GetMatches()
        {
            var lst = await GetAsync<MatchDto[]>("api/match");
            return lst ?? Enumerable.Empty<MatchDto>();
        }

        public async Task<List<ClubDto>> GetClubs()
        {
            var lst = await GetAsync<List<ClubDto>>("api/club");
            _log.LogInformation("GetClubs back");
            return lst ?? new List<ClubDto>();
        }

        public async Task<List<CourseDto>> GetCourses(int clubId)
        {
            var lst = await GetAsync<List<CourseDto>>($"api/club/{clubId:int}/course");
            _log.LogInformation("GetCourses back");
            return lst ?? new List<CourseDto>();
        }

        //public async Task<IEnumerable<ErrorMessageData>> GetErrorMessages()
        //{
        //    var lst = await GetAsync<ErrorMessageData[]>("api/errormessage?limit=0");
        //    return lst ?? Enumerable.Empty<ErrorMessageData>();
        //}

        //public async Task AddErrorMessage(string source, string text)
        //{
        //    var model = new ErrorMessageCreateDto
        //    {
        //        Source = source,
        //        Messagetext = text
        //    };

        //    var res = await _client.PostAsJsonAsync($"api/errormessage", model);
        //    res.EnsureSuccessStatusCode();
        //}


        //public async Task<HttpResponseMessage> DeleteErrorMessages()
        //{
        //    var ok = await _client.DeleteAsync("api/errormessage");
        //    return ok;
        //}

        //public async Task<HttpResponseMessage> DeleteLogBook()
        //{
        //    var ok = await _client.DeleteAsync("api/logbook");
        //    return ok;
        //}
        //#endregion
    }
}
