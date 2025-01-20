using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services
{
    public interface IMatchService
    {
        //string Baseaddress { get; }

        Task<MatchModel?> GetMatch(int matchId);
        Task<IEnumerable<MatchModel>?> GetMatches();
        Task<MatchModel> UpsertMatch(MatchModel match);
        Task<IEnumerable<MatchResult>?> GetMatchResults(int matchId);
        Task<IEnumerable<MatchBirdieResult>?> GetMatchBirdies(int matchId);
        Task<IEnumerable<MatchResult>?> MatchResultForRegistration(int matchId);
        Task<bool> UpsertResultMatch(MatchResult dto);
        Task<bool> MatchSettlement(int matchId);
        Task<bool> DeleteResultMatch(int matchResultId);
        Task<string> MatchRegistration(int matchResultId, string regFile);
        Task<IEnumerable<ListEntry>?> GetMatchforms();
    }
}