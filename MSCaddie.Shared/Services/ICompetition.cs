using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services;

public interface ICompetitionService
{
    Task<IEnumerable<CompetitionResult>> GetMatchCompetitions(int matchId);
    Task<IEnumerable<ListEntry>?> GetCompetitions();
    Task<bool> UpsertGetCompetitionResult(CompetitionUpsert dto);
    Task<bool> DeleteCompetitionResult(int resultId);
}