using MSCaddie.Shared.Dtos;

namespace MSCaddie.Shared.Interfaces;

public interface IMatchPlayRepository
{

    #region MatchPlay
    Task<IEnumerable<MatchPlayTeamDto>> MatchPlayTeamList();
    Task<IEnumerable<MatchPlayTeamDto>> GetMatchplays();
    Task<MatchPlayTeamDto> GetMatchplay(int matchId);

    //Task<IEnumerable<ListEntry>?> GetCompetitions();
    //Task<IEnumerable<CompetitionResult>> GetCompetitionResults(int matchId);
    //Task<int> UpsertCompetitionResult(CompetitionUpsertDto dto);
    //Task<int> DeleteCompetitionResult(int resultId);
    #endregion

}
