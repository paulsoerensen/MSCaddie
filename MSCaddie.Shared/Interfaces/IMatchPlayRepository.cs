using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Interfaces;

public interface IMatchPlayRepository
{

    #region MatchPlay
    Task<IEnumerable<PlayerForMatchPlayDto>> GetPlayersForMatchPlay();
    Task<IEnumerable<PlayerDto>> GetPlayersForMatchPlayPar();

    Task<IEnumerable<MatchPlayTeamDto>> MatchPlayTeamList(int leagueId);
    Task<IEnumerable<MatchPlayTeamDto>> GetMatchplays();
    Task<MatchPlayTeamDto> LeagueTeamUpsert(MatchPlayTeamDto model);
    Task DeleteMatchplayPar(int id);


    //Task<MatchPlayTeamDto> GetMatchplay(int matchId);
    //Task<IEnumerable<ListEntry>?> GetCompetitions();
    //Task<IEnumerable<CompetitionResult>> GetCompetitionResults(int matchId);
    //Task<int> UpsertCompetitionResult(CompetitionUpsertDto dto);
    //Task<int> DeleteCompetitionResult(int resultId);
    #endregion

}
