using MSCaddie.Shared.Containers;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;
using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services;
public class MatchPlayService : IMatchPlayService
{
    private const string BaseAddress = "api/matchplay";

    private readonly HttpClient _client;
    ILogger<MatchPlayService> _logger;

    public MatchPlayService(HttpClient client, ILogger<MatchPlayService> logger)
    {
        _client = client;
        _logger = logger;
    }
    public string Baseaddress => _client.BaseAddress?.ToString();

    public async Task<IEnumerable<LeagueMatch>?> GetMatchplays()
    {
        _logger.LogInformation($"Called GetMatchplays()");
        return await _client.GetFromJsonAsync<IEnumerable<LeagueMatch>>($"{BaseAddress}");
    }

    public async Task<LeagueMatch?> GetMatchplay(int matchId)
    {
        _logger.LogInformation($"Called GetMatchplay({matchId})");
        return await _client.GetFromJsonAsync<LeagueMatch>($"{BaseAddress}/{matchId}");
    }

    public async Task<IEnumerable<LeagueTeam>?> GetMatchPlayTeams()
    {
        _logger.LogInformation("Called GetMatchPlayTeams");
        return await _client.GetFromJsonAsync<IEnumerable<LeagueTeam>>($"{BaseAddress}/teams");
    }

}

