using MSCaddie.Repository.Models;

namespace MSCaddie.Repository.Interfaces
{
    public interface IMatchService
    {
        Task<MatchModel?> GetMatch(int matchId);
        Task<IEnumerable<MatchModel>?> GetMatches();
        Task<IEnumerable<MatchModel>?> GetMatches(DateTime start, DateTime end);
        Task<MatchModel> UpsertMatch(MatchModel match);
        Task<IEnumerable<MatchResultModel>?> GetMatchResults(int matchId);
        Task<IEnumerable<MatchResultModel>?> GetMatchBirdies(int matchId);
        Task<IEnumerable<MatchResultModel>?> MatchResultForRegistration(int matchId);
        Task<bool> UpsertResultMatch(MatchResultModel dto);
        Task<bool> MatchSettlement(int matchId);
        Task<bool> DeleteResultMatch(int matchResultId);
        Task<string> MatchRegistration(int matchResultId, string regFile);
        Task<IEnumerable<ListEntryModel>?> GetMatchforms();
        Task<NearestPinResultModel?> GetNearestPinResult(int nearestPinId);
        Task<IEnumerable<NearestPinResultModel>?> GetNearestPinResults(int matchId);
        Task<NearestPinResultModel> UpdateNearestPinResult(NearestPinResultModel model);
        Task<bool> DeleteNearestPinResult(int id);
    }
}