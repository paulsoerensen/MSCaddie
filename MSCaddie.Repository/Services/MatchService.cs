using MSCaddie.Repository.Interfaces;
using MSCaddie.Repository.Dtos;
using MSCaddie.Repository.Models;
using Microsoft.Extensions.Logging;
using MSCaddie.Repository.Interfaces;
using AutoMapper;

namespace MSCaddie.Repository.Services;
public class MatchService : IMatchService
{
    IMatchRepository _matchRepository;
    ILogger<MatchService> _logger;
    IMapper mapper;
    int season = 2000;

    public MatchService(IMatchRepository matchRepository,
        IAdminRepository adminRepo,
        ILogger<MatchService> logger)
    {
        _matchRepository = matchRepository;
        _logger = logger;
        season = adminRepo.Season;
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
            cfg.CreateMap<NearestPinResultDto, NearestPinResultModel>().ReverseMap();
        })
        .CreateMapper();
    }

    public async Task<NearestPinResultModel?> GetNearestPinResult(int nearestPinId)
    {
        NearestPinResultDto dto = await _matchRepository.GetNearestPinResult(nearestPinId);
        return mapper.Map<NearestPinResultModel>(dto);
    }

    public async Task<IEnumerable<NearestPinResultModel>?> GetNearestPinResults(int matchId)
    {
        IEnumerable<NearestPinResultDto> dtos = await _matchRepository.GetNearestPinResults(matchId);
        return mapper.Map<IEnumerable<NearestPinResultModel>>(dtos);
    }

    public async Task<NearestPinResultModel> UpdateNearestPinResult(NearestPinResultModel model)
    {
        NearestPinResultDto dto = mapper.Map<NearestPinResultDto>(model);
        dto = await _matchRepository.UpdateNearestPinResult(dto);
        return mapper.Map<NearestPinResultModel>(dto);
    }
    public async Task<bool> DeleteNearestPinResult(int id)
    {
        var i = await _matchRepository.DeleteNearestPinResult(id);
        return i > 0;
    }
    public async Task<IEnumerable<MatchModel>?> GetMatches()
    {
        IEnumerable<MatchDto> dtos = await _matchRepository.GetSeasonMatchList(season);
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

