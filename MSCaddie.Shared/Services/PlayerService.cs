using MSCaddie.Shared.Models;
using MSCaddie.Shared.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace MSCaddie.Shared.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _repo;
    private readonly IAdminRepository _repoAdmin;
    private readonly int season;

    public PlayerService(IPlayerRepository repo)
    {
        _repo = repo;
        season = _repoAdmin?.Season ?? DateTime.Now.Year;
    }
    public async Task<PlayerModel?> GetPlayer(int vgcno)
    {
        return await _repo.GetPlayer(vgcno);
        //return await _client.GetFromJsonAsync<PlayerDto>($"BaseAddress/{vgcno}");
    }
    public async Task<IEnumerable<PlayerModel?>?> GetPlayers()
    {
        var res =  await _repo.GetPlayers(season);
        return res?.Where(player => player?.Season == season);

        //return await _client.GetFromJsonAsync<IEnumerable<PlayerDto>>(BaseAddress);
    }
    public async Task<IEnumerable<PlayerModel?>?> GetNonMembers()
    {
        return await _repo.GetNonMembers(season);
        //return await _client.GetFromJsonAsync<IEnumerable<PlayerDto>>(BaseAddress);
    }
    public async Task<PlayerModel> UpsertPlayer(PlayerModel model)
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

