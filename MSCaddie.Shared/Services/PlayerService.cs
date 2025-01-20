using MSCaddie.Shared.Models;
using MSCaddie.Shared.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace MSCaddie.Shared.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _repo;

    public PlayerService(IPlayerRepository repo)
    {
        _repo = repo;
    }
    public async Task<Player?> GetPlayer(int vgcno)
    {
        return await _repo.GetPlayer(vgcno);
        //return await _client.GetFromJsonAsync<PlayerDto>($"BaseAddress/{vgcno}");
    }
    public async Task<IEnumerable<Player>?> GetPlayers()
    {
        return await _repo.GetPlayers(2024);
        //return await _client.GetFromJsonAsync<IEnumerable<PlayerDto>>(BaseAddress);
    }
    public async Task<Player> UpsertPlayer(Player model)
    {
        return await _repo.PlayerUpsert(model);
        //var response = await _client.PostAsJsonAsync<PlayerDto>($"{BaseAddress}", dto);
        //if (response.IsSuccessStatusCode)
        //{
        //    return await JsonSerializer.DeserializeAsync<PlayerDto>(await response.Content.ReadAsStreamAsync());
        //}
        //return null;
    }
}

