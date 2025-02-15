using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Interfaces;

public interface IMatchplayRepository
{

    #region Matchplay

    Task<IEnumerable<TeamSingleDto>> GetMatchplayTeams();
    Task<int> MatchplayTeamUpsert(TeamSingleDto model);
    Task<int> MatchplayTeamDelete(int id);


    Task<IEnumerable<PlayerForMatchplayDto>> GetPlayersForMatchplay();
    Task<IEnumerable<PlayerDto>> GetPlayersForMatchplayPar();

    Task<IEnumerable<MatchplayTeamDto>> MatchplayTeamList(int leagueId);
    Task<IEnumerable<MatchplayTeamDto>> GetMatchplays();
    Task DeleteMatchplayPar(int id);
    //Task<IEnumerable<MatchplayTeamDto>> GetMatchplayTeams(int leagueId);
    Task<IEnumerable<MatchTeamDto>> GetMatchTeams(int leagueId);


    //Task<MatchplayTeamDto> GetMatchplay(int matchId);
    //Task<IEnumerable<ListEntry>?> GetCompetitions();
    //Task<IEnumerable<CompetitionResult>> GetCompetitionResults(int matchId);
    //Task<int> UpsertCompetitionResult(CompetitionUpsertDto dto);
    //Task<int> DeleteCompetitionResult(int resultId);
    #endregion

}
