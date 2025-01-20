using  MSCaddie.Shared.Models;


namespace  MSCaddie.Shared.Interfaces;

public interface IPlayerRepository
{
    #region Player
    Task<IEnumerable<Player?>> GetPlayers(int season);
    Task<Player?> GetPlayer(int playerId);
    Task<Player> PlayerUpsert(Player model);
    #endregion

}
