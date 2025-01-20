using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services;

public interface IPlayerService
{
    Task<Player?> GetPlayer(int vgcno);
    Task<IEnumerable<Player>?> GetPlayers();
    Task<Player> UpsertPlayer(Player model);
}