using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Interfaces;

public interface IPlayerService
{
    Task<PlayerModel?> GetPlayer(int vgcno);
    Task<IEnumerable<PlayerModel>?>? GetPlayers();
    Task<IEnumerable<PlayerModel?>> GetNonMembers();
    Task<PlayerModel> UpsertPlayer(PlayerModel model);
}