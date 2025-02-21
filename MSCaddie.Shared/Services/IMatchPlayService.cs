using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services;

public interface IMatchplayService
{
    Task<IEnumerable<TeamSingleModel>> GetMatchplayTeams();
    Task <int> MatchplayTeamUpsert(TeamSingleModel model);
    Task<int> MatchplayTeamDelete(int id);


    Task<IEnumerable<PlayerModel>> GetTeamPartners();
    Task<IEnumerable<TeamParModel>> GetMatchplayTeamPars();
    Task<int> MatchplayTeamParUpsert(TeamParModel model);
    Task<int> MatchplayTeamParDelete(int id);

    // match fixing
    Task<IEnumerable<MatchplayTeamModel>> GetMatchplayTeams(char league);
    Task<IEnumerable<MatchplayGameModel>> GetMatchplayGames(char league);
    Task<int> MatchplayGameUpsert(MatchplayGameModel model);
    Task<int> MatchplayGameDelete(int id);


    Task<IEnumerable<PlayerForMatchplayModel>> GetPlayersForMatchplay();
    Task<IEnumerable<PlayerModel>> GetPlayersForMatchplayPar();

    Task DeleteMatchplayPar(PlayerForMatchplayModel model);

    //Task<LeagueMatch?> GetMatchplay(int matchId);
    //Task<IEnumerable<LeagueMatch>?> GetMatchplays();
    //Task<IEnumerable<MatchTeamModel>?> GetTeamsForMatchplay(int leagueId);
    //Task<IEnumerable<MatchTeamModel>> GetMatchTeams(int leagueId);


    //Task<MatchDto> UpsertMatch(MatchDto match);
    //Task<IEnumerable<MatchBirdieResultDto>> GetMatchBirdies(int matchId);
    //Task<IEnumerable<MatchResultDto>?> MatchResultForRegistration(int matchId);
    //Task<bool> UpsertResultMatch(MatchResultDto dto);
    //Task<bool> MatchSettlement(int matchId);
    //Task<bool> DeleteResultMatch(int matchResultId);
    //Task<string> MatchRegistration(int matchResultId, string regFile);
    //Task<IEnumerable<ListItem>?> GetMatchforms();
}