using MSCaddie.Shared.Dtos;
using System.Net.Http.Json;
using System.Text.Json;

namespace MSCaddie.Shared.Services;

public class PlayerService : IPlayerService
{
    private const string BaseAddress = "api/player";

    private readonly HttpClient _client;

    public PlayerService(HttpClient client)
    {
        _client = client;
    }
    public string Baseaddress => _client.BaseAddress?.ToString();

    public async Task<PlayerDto?> GetPlayer(int vgcno)
    {
        return await _client.GetFromJsonAsync<PlayerDto>($"BaseAddress/{vgcno}");
    }
    public async Task<IEnumerable<PlayerDto>?> GetPlayers()
    {
        return await _client.GetFromJsonAsync<IEnumerable<PlayerDto>>(BaseAddress);
    }
    public async Task<PlayerDto> UpsertPlayer(PlayerDto dto)
    {
        var response = await _client.PostAsJsonAsync<PlayerDto>($"{BaseAddress}", dto);
        if (response.IsSuccessStatusCode)
        {
            return await JsonSerializer.DeserializeAsync<PlayerDto>(await response.Content.ReadAsStreamAsync());
        }
        return null;
    }
}

