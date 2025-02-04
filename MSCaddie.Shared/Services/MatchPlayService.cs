using MSCaddie.Shared.Containers;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Interfaces;

namespace MSCaddie.Shared.Services;
public class MatchPlayService : IMatchPlayService
{
    private const string BaseAddress = "api/matchplay";

    IMatchPlayRepository _matchRepository;
    ILogger<MatchPlayService> _logger;

    public MatchPlayService(IMatchPlayRepository matchRepository,
        ILogger<MatchPlayService> logger)
    {
        _matchRepository = matchRepository;
        _logger = logger;
    }
    //public string Baseaddress => _client.BaseAddress?.ToString();

    public async Task<IEnumerable<LeagueMatch>?> GetMatchplays()
    {
        _logger.LogInformation($"Called GetMatchplays()");
        return await Task.FromResult(Enumerable.Empty<LeagueMatch>());

        //return await _matchRepository.GetMatchplays();
    }

    public async Task<LeagueMatch?> GetMatchplay(int matchId)
    {
        _logger.LogInformation($"Called GetMatchplay({matchId})");
        //return await _matchRepository.GetFromJsonAsync<LeagueMatch>($"{BaseAddress}/{matchId}");
        return await Task.FromResult<LeagueMatch?>(null);
    }

    public async Task<IEnumerable<LeagueTeam>?> GetMatchPlayTeams()
    {
        _logger.LogInformation("Called GetMatchPlayTeams");
        //return await _matchRepository.GetFromJsonAsync<IEnumerable<LeagueTeam>>($"{BaseAddress}/teams");
        return await Task.FromResult(Enumerable.Empty<LeagueTeam>());
    }

}

