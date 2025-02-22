using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Interfaces;

public interface IMatchRepository
{

    #region Competition
    Task<IEnumerable<ListEntryModel>?> GetCompetitions();
    Task<IEnumerable<CompetitionResultDto>> GetCompetitionResults(int matchId);
    Task<int> UpsertCompetitionResult(CompetitionResultDto dto);
    Task<int> DeleteCompetitionResult(int resultId);
    #endregion

    #region Match
    Task<MatchModel?> GetMatch(int matchId);
    Task<IEnumerable<MatchModel>> GetMatchList();
    Task<IEnumerable<MatchModel>> GetSeasonMatchList(int season);
    Task<MatchModel> MatchUpsert(MatchModel dto);

    Task<IEnumerable<MatchResult>> GetMatchResults(int matchId);
    Task<IEnumerable<MatchResult>?> GetMatchResultForRegistration(int matchId);
    Task<MatchResult> MatchResultUpsert(MatchResult model);
    Task<int> MatchRegistrationUpsert(MatchRegistration model);
    Task<int> MatchResultDelete(int id);
    Task<IEnumerable<MatchBirdieResult>?> GetMatchBirdies(int matchId);
    Task<int> MatchResultSettlement(int matchId);
    Task<IEnumerable<ListEntryModel>> GetMatchforms();

    #endregion

}
