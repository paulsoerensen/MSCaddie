using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Models;
using Microsoft.Extensions.Logging;
using MSCaddie.Shared.Interfaces;
using AutoMapper;

namespace MSCaddie.Shared.Services;
public class MatchService : IMatchService
{
    private const string BaseAddress = "api/match";

    IMatchRepository _matchRepository;
    ILogger<MatchService> _logger;
    IMapper mapper;

    public MatchService(IMatchRepository matchRepository,
        ILogger<MatchService> logger)
    {
        _matchRepository = matchRepository;
        _logger = logger;
        mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CompetitionResultDto, CompetitionResultModel>().ReverseMap();
            cfg.CreateMap<ListItem, ListEntryModel>().ReverseMap()
                .ForMember(dest => dest.KeyId, opt =>
                    opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.KeyValue, opt =>
                    opt.MapFrom(src => src.Value));
        })
        .CreateMapper();
    }
    //public string Baseaddress => _client.BaseAddress?.ToString();

    public async Task<IEnumerable<MatchModel>?> GetMatches()
    {
        return await _matchRepository.GetMatchList();
        //return await _client.GetFromJsonAsync<IEnumerable<Match>>(BaseAddress);
    }

    public async Task<MatchModel?> GetMatch(int matchId)
    {
        return await _matchRepository.GetMatch(matchId);
        //return await _client.GetFromJsonAsync<Match>($"{BaseAddress}/{matchId}");
    }

    public async Task<IEnumerable<MatchResult>?> GetMatchResults(int matchId)
    {
        _logger.LogInformation("Called GetMatchResults");
        return await _matchRepository.GetMatchResults(matchId);
        //return await _client.GetFromJsonAsync<IEnumerable<MatchResultDto>>($"{BaseAddress}/{matchId}/result");
    }

    public async Task<IEnumerable<MatchResult>?> MatchResultForRegistration(int matchId)
    {
        _logger.LogInformation($"Called MatchResultForRegistration({matchId})");
        return await _matchRepository.GetMatchResultForRegistration(matchId);
        //return await _client.GetFromJsonAsync<IEnumerable<MatchResultDto>>($"{BaseAddress}/{matchId}/resultregistration");
    }

    public async Task<bool> MatchSettlement(int matchId)
    {
        _logger.LogInformation("Called MatchSettlement");
        var i = await _matchRepository.MatchResultSettlement(matchId);
        return i > 0;
        //var response = await _client.PostAsJsonAsync<int>($"{BaseAddress}/{matchId}/settlement", matchId);
        //return response.IsSuccessStatusCode;

    }

    public async Task<MatchModel> UpsertMatch(MatchModel dto)
    {
        return await _matchRepository.MatchUpsert(dto);
        //var response = await _client.PostAsJsonAsync<Match>($"{BaseAddress}", dto);
        //if (response.IsSuccessStatusCode)
        //{
        //    return await JsonSerializer.DeserializeAsync<Match>(await response.Content.ReadAsStreamAsync());
        //}
        //return null;
    }

    public async Task<bool> UpsertResultMatch(MatchResult dto)
    {
        await _matchRepository.MatchResultUpsert(dto);
        //var res = await _client.PostAsJsonAsync<MatchResultDto>($"{BaseAddress}/result", dto);
        return true; // res.IsSuccessStatusCode;
    }
    public async Task<bool> DeleteResultMatch(int matchResultId)
    {
        var i = await _matchRepository.MatchResultDelete(matchResultId);
        return i > 0;
        //var res = await _client.DeleteAsync($"{BaseAddress}/result/{matchResultId}");
        //return res.IsSuccessStatusCode;
    }
    public async Task<string>MatchRegistration(int matchId, string regFile)
    {
        return "";
        //return await _matchRepository.MatchResultUpsert(matchId, regFile);
        //var res = await _client.PostAsJsonAsync<string>($"{BaseAddress}/{matchId}/registration", regFile);
        //return res.ToString();
    }

    public async Task<IEnumerable<ListEntryModel>?> GetMatchforms()
    {
        return await _matchRepository.GetMatchforms();
        //return await _client.GetFromJsonAsync<IEnumerable<ListItem>>($"api/matchform");
    }

    public async Task<IEnumerable<MatchBirdieResult>?> GetMatchBirdies(int matchId)
    {
        return await _matchRepository.GetMatchBirdies(matchId);
        //return await _client.GetFromJsonAsync<IEnumerable<MatchBirdieResultDto>>($"api/match/{matchId}/birdies");
    }

}

