using MSCaddie.Shared.Dtos;

namespace MSCaddie.Shared.Services
{
    public interface IMatchService
    {
        string Baseaddress { get; }

        Task<MatchDto?> GetMatch(int matchId);
        Task<IEnumerable<MatchDto>?> GetMatches();
        Task<MatchDto> UpsertMatch(MatchDto match);
        Task<IEnumerable<MatchResultDto>?> GetMatchResults(int matchId);
        Task<IEnumerable<MatchBirdieResultDto>> GetMatchBirdies(int matchId);
        Task<IEnumerable<MatchResultDto>?> MatchResultForRegistration(int matchId);
        Task<bool> UpsertResultMatch(MatchResultDto dto);
        Task<bool> MatchSettlement(int matchId);
        Task<bool> DeleteResultMatch(int matchResultId);
        Task<string> MatchRegistration(int matchResultId, string regFile);
        Task<IEnumerable<ListItem>?> GetMatchforms();
    }
}