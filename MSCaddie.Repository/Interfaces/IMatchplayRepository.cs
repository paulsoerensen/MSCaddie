using MSCaddie.Repository.Dtos;


namespace MSCaddie.Repository.Interfaces;

public interface IMatchplayRepository
{

    #region Matchplay

    Task<IEnumerable<TeamSingleDto>> GetMatchplayTeams();
    Task<int> MatchplayTeamUpsert(TeamSingleDto model);
    Task<int> MatchplayTeamDelete(int id);


    Task<IEnumerable<PlayerDto>> GetTeamPartners();
    Task<IEnumerable<TeamParDto>> GetMatchplayTeamPars();
    Task<int> MatchplayTeamParUpsert(TeamParDto model);
    Task<int> MatchplayTeamParDelete(int id);


    Task<IEnumerable<MatchplayTeamDto>> GetMatchplayTeams(char league);

    // games
    Task<IEnumerable<MatchplayGameDto>> GetMatchplayGames(char league);
    Task<IEnumerable<MatchplayGameDto>> GetLatestMatchplays(int top);
    Task<int> MatchplayGameUpsert(MatchplayGameDto model);
    Task<int> MatchplayGameDelete(int id);


    Task<IEnumerable<MatchplayGameDto>> GetMatchplayGamePars(char league);
    Task<int> MatchplayGameParUpsert(MatchplayGameDto model);
    Task<int> MatchplayGameParDelete(int id);

    Task<IEnumerable<PlayerForMatchplayDto>> GetPlayersForMatchplay();
    Task<IEnumerable<PlayerDto>> GetPlayersForMatchplayPar();

    Task<IEnumerable<MatchplayTeamDto>> MatchplayTeamList(int leagueId);
    Task<IEnumerable<MatchplayTeamDto>> GetMatchplays();
    Task DeleteMatchplayPar(int id);
    #endregion

}
