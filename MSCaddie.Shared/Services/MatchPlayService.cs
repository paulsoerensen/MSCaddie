using AutoMapper;
using Microsoft.Extensions.Logging;
using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Interfaces;
using MSCaddie.Shared.Models;
using System.Reflection;

namespace MSCaddie.Shared.Services;
public class MatchplayService : IMatchplayService
{
    private const string BaseAddress = "api/matchplay";

    IMatchplayRepository _matchRepository;
    ILogger<MatchplayService> _logger;
    IMapper mapper;

    public MatchplayService(IMatchplayRepository matchRepository,
        ILogger<MatchplayService> logger)
    {
        _matchRepository = matchRepository;
        _logger = logger;
        mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TeamSingleDto, TeamSingleModel>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Lastname == null ? src.Firstname : $"{src.Firstname} {src.Lastname}"));

            // Mapping from TeamSingleModel to TeamSingleDto
            cfg.CreateMap<TeamSingleModel, TeamSingleDto>()
                .ForMember(dest => dest.League, opt => opt.MapFrom(src => MapIntToChar(src.LeagueInt)));
            cfg.CreateMap<TeamSingleDto, TeamSingleModel>()
                .ForMember(dest => dest.TeamName, opt => 
                    opt.MapFrom(src => src.Lastname == null ? src.Firstname : $"{src.Firstname} {src.Lastname}"))
                .ForMember(dest => dest.LeagueInt, opt => opt.MapFrom(src => MapCharToInt(src.League)));


        cfg.CreateMap<PlayerDto, PlayerModel>();
            cfg.CreateMap<PlayerForMatchplayDto, PlayerForMatchplayModel>();
            cfg.CreateMap<PlayerForMatchplayModel, MatchplayTeamDto>()
                .ForMember(dest => dest.TeamName,
                        opts => opts.MapFrom(src => src.Fullname))
                .ForMember(dest => dest.TeamName2,
                        opts => opts.MapFrom(src => $"{src.Fullname}, {src.Fullname2}"));
            cfg.CreateMap<PlayerForMatchplayModel, MatchTeamDto>().ReverseMap();
            cfg.CreateMap<MatchTeamModel, MatchTeamDto>().ReverseMap(); 
        })
        .CreateMapper();

    }

    private char? MapIntToChar(int? number)
    {
        return number switch
        {
            1 => 'A',
            2 => 'B',
            _ => null
        };
    }
    private int? MapCharToInt(char? ch)
    {
        return ch switch
        {
            'A' => 1,
            'B' => 2,
            _ => null
        };
    }


    #region Matchplay teams single
    public async Task<IEnumerable<TeamSingleModel>> GetMatchplayTeams()
    {
        _logger.LogInformation($"Called GetMatchplayTeams()");
        IEnumerable<TeamSingleDto> dtos = await _matchRepository.GetMatchplayTeams();
        return mapper.Map<IEnumerable<TeamSingleModel>>(dtos);
    }

    public async Task<int> MatchplayTeamUpsert(TeamSingleModel model)
    {
        _logger.LogInformation($"Called MatchplayTeamUpsert()");
        TeamSingleDto dto = mapper.Map<TeamSingleDto>(model);

        return await _matchRepository.MatchplayTeamUpsert(dto);
    }
    public async Task<int> MatchplayTeamDelete(int id)
    {
        _logger.LogInformation($"Called MatchplayTeamDelete()");
        return await _matchRepository.MatchplayTeamDelete(id);
    }

    #endregion


    public async Task<IEnumerable<PlayerForMatchplayModel>> GetPlayersForMatchplay()
    {
        _logger.LogInformation($"Called GetPlayersForMatchplay()");
        IEnumerable<PlayerForMatchplayDto> dtos = await _matchRepository.GetPlayersForMatchplay();
        return mapper.Map<IEnumerable<PlayerForMatchplayModel>>(dtos);
    }
    public async Task<IEnumerable<PlayerModel>> GetPlayersForMatchplayPar()
    {
        _logger.LogInformation($"Called GetPlayersForMatchplay()");
        IEnumerable<PlayerDto> dtos = await _matchRepository.GetPlayersForMatchplayPar();
        return mapper.Map<IEnumerable<PlayerModel>>(dtos);
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

    public async Task DeleteMatchplayPar(PlayerForMatchplayModel model)
    {
        await _matchRepository.DeleteMatchplayPar(model.LeagueTeamId);
    }
    public async Task<IEnumerable<MatchTeamModel>?> GetTeamsForMatchplay(int leagueId)
    {
        _logger.LogInformation("Called GetMatchTeams");
        var dtos = await _matchRepository.GetMatchplayTeams();
        //return mapper.Map<IEnumerable<MatchTeamModel>>(dtos);
        return null;
    }



    public async Task<IEnumerable<MatchTeamModel>?> GetMatchTeams(int leagueId)
    {
        _logger.LogInformation("Called GetMatchTeams");
        var dtos = await _matchRepository.GetMatchTeams(leagueId);
        return mapper.Map< IEnumerable<MatchTeamModel>>(dtos);
    }

}

