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
            cfg.CreateMap<MatchDto, MatchModel>().ReverseMap();
            cfg.CreateMap<MatchResultDto, MatchResultModel>().ReverseMap();
            cfg.CreateMap<ListEntryDto, ListEntryModel>()
                .ForMember(dest => dest.Key, opt =>
                    opt.MapFrom(src => src.KeyId))
                .ForMember(dest => dest.Value, opt =>
                    opt.MapFrom(src => src.KeyValue));
            cfg.CreateMap<ListEntryModel, ListEntryDto>()
                .ForMember(dest => dest.KeyId, opt =>
                    opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.KeyValue, opt =>
                    opt.MapFrom(src => src.Value));
        })
        .CreateMapper();
    }

    public async Task<IEnumerable<MatchModel>?> GetMatches()
    {
        IEnumerable<MatchDto> dtos = await _matchRepository.GetMatchList();
        return mapper.Map<IEnumerable<MatchModel>>(dtos);
        //return await _client.GetFromJsonAsync<IEnumerable<Match>>(BaseAddress);
    }

    public async Task<MatchModel?> GetMatch(int id)
    {
        MatchDto? dto = await _matchRepository.GetMatch(id);
        return mapper.Map<MatchModel>(dto);
        //return await _client.GetFromJsonAsync<IEnumerable<Match>>(BaseAddress);
    }

    public async Task<IEnumerable<MatchResultModel>?> GetMatchResults(int matchId)
    {
        _logger.LogInformation("Called GetMatchResults");
        IEnumerable<MatchResultDto> dtos = await _matchRepository.GetMatchResults(matchId);
        return mapper.Map<IEnumerable<MatchResultModel>>(dtos);
        //return await _client.GetFromJsonAsync<IEnumerable<MatchResultDto>>($"{BaseAddress}/{matchId}/result");
    }

    public async Task<IEnumerable<MatchResultModel>?> MatchResultForRegistration(int matchId)
    {
        _logger.LogInformation($"Called MatchResultForRegistration({matchId})");
        IEnumerable<MatchResultDto> dtos = await _matchRepository.GetMatchResultForRegistration(matchId);
        return mapper.Map<IEnumerable<MatchResultModel>>(dtos);
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

    public async Task<MatchModel> UpsertMatch(MatchModel model)
    {
        MatchDto dto = mapper.Map<MatchDto>(model);
        dto = await _matchRepository.MatchUpsert(dto);
        return mapper.Map<MatchModel>(dto);
    }

    public async Task<bool> UpsertResultMatch(MatchResultModel model)
    {
        MatchResultDto dto = mapper.Map<MatchResultDto>(model);
        await _matchRepository.MatchResultUpsert(dto);
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
        IEnumerable<ListEntryDto> dtos = await _matchRepository.GetMatchforms();
        return mapper.Map<IEnumerable<ListEntryModel>>(dtos);
        //return await _client.GetFromJsonAsync<IEnumerable<ListItem>>($"api/matchform");
    }

    public async Task<IEnumerable<MatchResultModel>?> GetMatchBirdies(int matchId)
    {
        var dtos =  await _matchRepository.GetMatchBirdies(matchId);
        return mapper.Map<IEnumerable<MatchResultModel>>(dtos);
        //return await _client.GetFromJsonAsync<IEnumerable<MatchBirdieResultDto>>($"api/match/{matchId}/birdies");
    }

}

