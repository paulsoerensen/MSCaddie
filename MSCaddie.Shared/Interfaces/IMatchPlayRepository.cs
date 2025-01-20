using MSCaddie.Shared.Models;

namespace  MSCaddie.Shared.Interfaces;

public interface IMatchPlayRepository
{

    #region MatchPlay
    Task<IEnumerable<LeagueTeam>> MatchPlayTeamList();
    Task<IEnumerable<LeagueMatch>> GetMatchplays();
    Task<LeagueMatch> GetMatchplay(int matchId);

    //Task<IEnumerable<ListEntry>?> GetCompetitions();
    //Task<IEnumerable<CompetitionResult>> GetCompetitionResults(int matchId);
    //Task<int> UpsertCompetitionResult(CompetitionUpsertDto dto);
    //Task<int> DeleteCompetitionResult(int resultId);
    #endregion

}
