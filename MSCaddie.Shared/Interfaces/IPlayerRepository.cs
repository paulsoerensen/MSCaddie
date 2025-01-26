using  MSCaddie.Shared.Models;


namespace  MSCaddie.Shared.Interfaces;

public interface IPlayerRepository
{
    #region Player
    Task<IEnumerable<PlayerModel?>> GetPlayers(int season);
    Task<IEnumerable<PlayerModel?>?> GetNonMembers(int season);

    Task<PlayerModel?> GetPlayer(int playerId);
    Task<PlayerModel> PlayerUpsert(PlayerModel model);
    #endregion

}
