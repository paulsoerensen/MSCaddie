using MSCaddie.Repository.Models;

namespace MSCaddie.Repository.Interfaces;

public interface ICompetitionService
{
    Task<IEnumerable<CompetitionResultModel>> GetMatchCompetitionResults(int matchId);
    Task<IEnumerable<ListEntryModel>?> GetCompetitions();
    Task<CompetitionResultModel> GetCompetitionResultModel(string text);

    Task<bool> UpsertGetCompetitionResult(CompetitionResultModel dto);
    Task<bool> DeleteCompetitionResult(int resultId);
}