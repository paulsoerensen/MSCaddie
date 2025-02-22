using AutoMapper;
using Microsoft.Extensions.Logging;
using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Interfaces;
using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services;
public class CompetitionService : ICompetitionService
{
    ILogger<CompetitionService> _logger;
    private readonly IMatchRepository _repo;
    IMapper mapper;

    public CompetitionService(IMatchRepository repo, ILogger<CompetitionService> logger)
    {
        _logger = logger;
        _repo = repo;
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

    public async Task<IEnumerable<CompetitionResultModel>?> GetMatchCompetitionResults(int matchId)
    {
        _logger.LogInformation($"Called GetCompetitionResults()");
        var dtos = await _repo.GetCompetitionResults(matchId);
        return mapper.Map<IEnumerable<CompetitionResultModel>>(dtos);
    }
    public async Task<IEnumerable<ListEntryModel>?> GetCompetitions()
    {
        var dtos = await _repo.GetCompetitions();
        return mapper.Map<IEnumerable<ListEntryModel>>(dtos);
    }
    public async Task<bool> UpsertGetCompetitionResult(CompetitionResultModel model)
    {
        var dto = mapper.Map<CompetitionResultDto>(model);
        int i = await _repo.UpsertCompetitionResult(dto);
        return i > 0;
    }
    public async Task<bool> DeleteCompetitionResult(int id)
    {
        int i = await _repo.DeleteCompetitionResult(id);
        return i > 0;
    }
}

