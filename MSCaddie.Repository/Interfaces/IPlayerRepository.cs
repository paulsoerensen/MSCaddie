using MSCaddie.Repository.Dtos;
using  MSCaddie.Repository.Models;

namespace  MSCaddie.Repository.Interfaces;

public interface IPlayerRepository
{
    #region Player
    Task<IEnumerable<PlayerModel?>> GetPlayers(int season);
    Task<IEnumerable<PlayerModel?>?> GetNonMembers(int season);

    Task<PlayerModel?> GetPlayer(int playerId);
    Task<PlayerModel> PlayerUpsert(PlayerModel model);
    #endregion

    #region MemberShip
    Task<MembershipDto> MembershipUpsert(PlayerModel model);
    #endregion

}
