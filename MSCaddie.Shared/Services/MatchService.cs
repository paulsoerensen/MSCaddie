using MSCaddie.Shared.Containers;
using MSCaddie.Shared.Dtos;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace MSCaddie.Shared.Services;
public class MatchService : IMatchService
{
    private const string BaseAddress = "api/match";

    private readonly HttpClient _client;
    ILogger<MatchService> _logger;

    public MatchService(HttpClient client, ILogger<MatchService> logger)
    {
        _client = client;
        _logger = logger;
    }
    public string Baseaddress => _client.BaseAddress?.ToString();

    public async Task<IEnumerable<MatchDto>?> GetMatches()
    {
        return await _client.GetFromJsonAsync<IEnumerable<MatchDto>>(BaseAddress);
    }

    public async Task<MatchDto?> GetMatch(int matchId)
    {
        return await _client.GetFromJsonAsync<MatchDto>($"{BaseAddress}/{matchId}");
    }

    public async Task<IEnumerable<MatchResultDto>?> GetMatchResults(int matchId)
    {
        _logger.LogInformation("Called GetMatchResults");
        return await _client.GetFromJsonAsync<IEnumerable<MatchResultDto>>($"{BaseAddress}/{matchId}/result");
    }

    public async Task<IEnumerable<MatchResultDto>?> MatchResultForRegistration(int matchId)
    {
        _logger.LogInformation($"Called MatchResultForRegistration({matchId})");
        return await _client.GetFromJsonAsync<IEnumerable<MatchResultDto>>($"{BaseAddress}/{matchId}/resultregistration");
    }

    public async Task<bool> MatchSettlement(int matchId)
    {
        _logger.LogInformation("Called MatchSettlement");
        var response = await _client.PostAsJsonAsync<int>($"{BaseAddress}/{matchId}/settlement", matchId);
        return response.IsSuccessStatusCode;

    }

    public async Task<MatchDto> UpsertMatch(MatchDto dto)
    {
        var response = await _client.PostAsJsonAsync<MatchDto>($"{BaseAddress}", dto);
        if (response.IsSuccessStatusCode)
        {
            return await JsonSerializer.DeserializeAsync<MatchDto>(await response.Content.ReadAsStreamAsync());
        }
        return null;
    }

    public async Task<bool> UpsertResultMatch(MatchResultDto dto)
    {
        var res = await _client.PostAsJsonAsync<MatchResultDto>($"{BaseAddress}/result", dto);
        return res.IsSuccessStatusCode;
    }
    public async Task<bool> DeleteResultMatch(int matchResultId)
    {
        var res = await _client.DeleteAsync($"{BaseAddress}/result/{matchResultId}");
        return res.IsSuccessStatusCode;
    }
    public async Task<string>MatchRegistration(int matchId, string regFile)
    {
        var res = await _client.PostAsJsonAsync<string>($"{BaseAddress}/{matchId}/registration", regFile);
        return res.ToString();
    }

    public async Task<IEnumerable<ListItem>?> GetMatchforms()
    {
        return await _client.GetFromJsonAsync<IEnumerable<ListItem>>($"api/matchform");
    }

    public async Task<IEnumerable<MatchBirdieResultDto>?> GetMatchBirdies(int matchId)
    {
        return await _client.GetFromJsonAsync<IEnumerable<MatchBirdieResultDto>>($"api/match/{matchId}/birdies");
    }
    public async Task<IEnumerable<CompetitionResultDto>?> GetMatchCompetitions(int matchId)
    {
        return await _client.GetFromJsonAsync<IEnumerable<CompetitionResultDto>?>($"api/match/{matchId}/CompetitionResults");
    }
    public async Task<IEnumerable<ListItem>?> GetCompetitions()
    {
        var res = await _client.GetFromJsonAsync<IEnumerable<ListItem>>($"api/competitions");
        return res;
    }
}

