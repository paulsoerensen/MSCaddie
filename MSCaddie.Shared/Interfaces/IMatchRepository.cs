using MSCaddie.Shared.Dtos;

namespace MSCaddie.Shared.Interfaces;

public interface IMatchRepository
{

    #region Competition
    Task<IEnumerable<ListEntryDto>?> GetCompetitions();
    Task<IEnumerable<CompetitionResultDto>> GetCompetitionResults(int matchId);
    Task<int> UpsertCompetitionResult(CompetitionResultDto dto);
    Task<int> DeleteCompetitionResult(int resultId);
    #endregion

    #region Match
    Task<MatchDto?> GetMatch(int matchId);
    Task<IEnumerable<MatchDto>> GetMatchList();
    Task<IEnumerable<MatchDto>> GetSeasonMatchList(int season);
    Task<MatchDto> MatchUpsert(MatchDto dto);

    Task<IEnumerable<MatchResultDto>> GetMatchResults(int matchId);
    Task<IEnumerable<MatchResultDto>?> GetMatchResultForRegistration(int matchId);
    Task<MatchResultDto> MatchResultUpsert(MatchResultDto dto);
    Task<int> MatchRegistrationUpsert(MatchRegistrationDto dto);
    Task<int> MatchResultDelete(int id);
    Task<IEnumerable<MatchResultDto>?> GetMatchBirdies(int matchId);
    Task<int> MatchResultSettlement(int matchId);
    Task<IEnumerable<ListEntryDto>> GetMatchforms();

    #endregion

}
