using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services;

public interface ICompetitionService
{
    Task<IEnumerable<CompetitionResultModel>> GetMatchCompetitionResults(int matchId);
    Task<IEnumerable<ListEntryModel>?> GetCompetitions();
    Task<bool> UpsertGetCompetitionResult(CompetitionResultModel dto);
    Task<bool> DeleteCompetitionResult(int resultId);
}