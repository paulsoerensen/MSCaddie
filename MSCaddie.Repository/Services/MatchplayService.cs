using AutoMapper;
using Microsoft.Extensions.Logging;
using MSCaddie.Repository.Interfaces;
using MSCaddie.Repository.Dtos;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Interfaces;

namespace MSCaddie.Repository.Services;
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

            cfg.CreateMap<TeamParModel, TeamParDto>().ReverseMap();

            // Match fixing
            cfg.CreateMap<MatchplayTeamDto, MatchplayTeamModel>().ReverseMap();
            cfg.CreateMap<MatchplayGameDto, MatchplayGameModel>().ReverseMap();
            

            cfg.CreateMap<PlayerDto, PlayerModel>();
            cfg.CreateMap<PlayerForMatchplayDto, PlayerForMatchplayModel>();

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
            3 => 'X',
            _ => null
        };
    }
    private int? MapCharToInt(char? ch)
    {
        return ch switch
        {
            'A' => 1,
            'B' => 2,
            'X' => 3,
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

    #region Matchplay teams par

    public async Task<IEnumerable<PlayerModel>> GetTeamPartners()
    {
        _logger.LogInformation($"Called GetMatchplayTeams()");
        IEnumerable<PlayerDto> dtos = await _matchRepository.GetTeamPartners();
        return mapper.Map<IEnumerable<PlayerModel>>(dtos);
    }

    public async Task<IEnumerable<TeamParModel>> GetMatchplayTeamPars()
    {
        _logger.LogInformation($"Called GetMatchplayTeamPars()");
        IEnumerable<TeamParDto> dtos = await _matchRepository.GetMatchplayTeamPars();
        return mapper.Map<IEnumerable<TeamParModel>>(dtos);
    }

    public async Task<int> MatchplayTeamParUpsert(TeamParModel model)
    {
        _logger.LogInformation($"Called MatchplayTeamUpsert()");
        TeamParDto dto = mapper.Map<TeamParDto>(model);

        return await _matchRepository.MatchplayTeamParUpsert(dto);
    }
    public async Task<int> MatchplayTeamParDelete(int id)
    {
        _logger.LogInformation($"Called MatchplayTeamParDelete()");
        return await _matchRepository.MatchplayTeamParDelete(id);
    }

    #endregion
    
    #region Match fixing

    public async Task<IEnumerable<MatchplayTeamModel>> GetMatchplayTeams(char league)
    {
        _logger.LogInformation($"Called GetMatchplayTeams()");
        IEnumerable<MatchplayTeamDto> dtos = await _matchRepository.GetMatchplayTeams(league);
        return mapper.Map<IEnumerable<MatchplayTeamModel>>(dtos);
    }

    public async Task<IEnumerable<MatchplayGameModel>> GetMatchplayGames(char league)
    {
        _logger.LogInformation($"Called GetMatchplayGames()");
        IEnumerable<MatchplayGameDto> dtos = await _matchRepository.GetMatchplayGames(league);
        return mapper.Map<IEnumerable<MatchplayGameModel>>(dtos);
    }

    public async Task<int> MatchplayGameUpsert(MatchplayGameModel model)
    {
        _logger.LogInformation($"Called MatchplayTeamUpsert()");
        MatchplayGameDto dto = mapper.Map<MatchplayGameDto>(model);

        return await _matchRepository.MatchplayGameUpsert(dto);
    }
    public async Task<int> MatchplayGameDelete(int id)
    {
        _logger.LogInformation($"Called MatchplayGameDelete()");
        return await _matchRepository.MatchplayGameDelete(id);
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

    public async Task DeleteMatchplayPar(PlayerForMatchplayModel model)
    {
        await _matchRepository.DeleteMatchplayPar(model.LeagueTeamId);
    }
}

