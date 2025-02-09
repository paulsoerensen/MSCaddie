using AutoMapper;
using Microsoft.Extensions.Logging;
using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Interfaces;
using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services;
public class MatchPlayService : IMatchPlayService
{
    private const string BaseAddress = "api/matchplay";

    IMatchPlayRepository _matchRepository;
    ILogger<MatchPlayService> _logger;
    IMapper mapper;

    public MatchPlayService(IMatchPlayRepository matchRepository,
        ILogger<MatchPlayService> logger)
    {
        _matchRepository = matchRepository;
        _logger = logger;
        mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<PlayerDto, PlayerModel>();
            cfg.CreateMap<PlayerForMatchPlayDto, PlayerForMatchPlayModel>();
            cfg.CreateMap<PlayerForMatchPlayModel, MatchPlayTeamDto>()
                .ForMember(dest => dest.TeamName,
                        opts => opts.MapFrom(src => src.Fullname))
                .ForMember(dest => dest.TeamName2,
                        opts => opts.MapFrom(src => $"{src.Fullname}, {src.Fullname2}"));
        })
        .CreateMapper();

    }
    public async Task<IEnumerable<PlayerForMatchPlayModel>> GetPlayersForMatchPlay()
    {
        _logger.LogInformation($"Called GetPlayersForMatchPlay()");
        IEnumerable<PlayerForMatchPlayDto> dtos = await _matchRepository.GetPlayersForMatchPlay();
        return mapper.Map<IEnumerable<PlayerForMatchPlayModel>>(dtos);
    }
    public async Task<IEnumerable<PlayerModel>> GetPlayersForMatchPlayPar()
    {
        _logger.LogInformation($"Called GetPlayersForMatchPlay()");
        IEnumerable<PlayerDto> dtos = await _matchRepository.GetPlayersForMatchPlayPar();
        return mapper.Map<IEnumerable<PlayerModel>>(dtos);
    }

    public async Task MatchPlayTeamUpsert(PlayerForMatchPlayModel model)
    {
        _logger.LogInformation($"Called MatchPlayTeamUpsert()");
        MatchPlayTeamDto dto = mapper.Map<MatchPlayTeamDto>(model);

        await _matchRepository.LeagueTeamUpsert(dto);
    }

    public async Task<IEnumerable<LeagueMatch>?> GetMatchplays()
    {
        _logger.LogInformation($"Called GetMatchplays()");
        return await Task.FromResult(Enumerable.Empty<LeagueMatch>());

        //return await _matchRepository.GetMatchplays();
    }

    public async Task<LeagueMatch?> GetMatchplay(int matchId)
    {
        _logger.LogInformation($"Called GetMatchplay({matchId})");
        //return await _matchRepository.GetFromJsonAsync<LeagueMatch>($"{BaseAddress}/{matchId}");
        return await Task.FromResult<LeagueMatch?>(null);
    }

    public async Task DeleteMatchPlayPar(PlayerForMatchPlayModel model)
    {
        await _matchRepository.DeleteMatchplayPar(model.LeagueTeamId);
    }

    public async Task<IEnumerable<LeagueTeam>?> GetMatchPlayTeams()
    {
        _logger.LogInformation("Called GetMatchPlayTeams");
        //return await _matchRepository.GetFromJsonAsync<IEnumerable<LeagueTeam>>($"{BaseAddress}/teams");
        return await Task.FromResult(Enumerable.Empty<LeagueTeam>());
    }

}

