using MSCaddie.Shared.Dtos;

namespace MSCaddie.Shared.Services
{
    public interface IPlayerService
    {
        string Baseaddress { get; }
        Task<PlayerDto?> GetPlayer(int vgcno);
        Task<IEnumerable<PlayerDto>?> GetPlayers();
        Task<PlayerDto> UpsertPlayer(PlayerDto match);
    }
}