using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services
{
    public interface IMatchPlayService
    {
        string Baseaddress { get; }


        Task<LeagueMatch?> GetMatchplay(int matchId);
        Task<IEnumerable<LeagueMatch>?> GetMatchplays();
        Task<IEnumerable<LeagueTeam>?> GetMatchPlayTeams();
        //Task<MatchDto> UpsertMatch(MatchDto match);
        //Task<IEnumerable<MatchBirdieResultDto>> GetMatchBirdies(int matchId);
        //Task<IEnumerable<MatchResultDto>?> MatchResultForRegistration(int matchId);
        //Task<bool> UpsertResultMatch(MatchResultDto dto);
        //Task<bool> MatchSettlement(int matchId);
        //Task<bool> DeleteResultMatch(int matchResultId);
        //Task<string> MatchRegistration(int matchResultId, string regFile);
        //Task<IEnumerable<ListItem>?> GetMatchforms();
    }
}