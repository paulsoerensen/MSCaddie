using MSCaddie.Shared.Containers;
using MSCaddie.Shared.Dtos;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace MSCaddie.Shared.Services;
public class CompetitionService : ICompetitionService
{
    private const string BaseAddress = "api/match";

    private readonly HttpClient _client;
    ILogger<CompetitionService> _logger;

    public CompetitionService(HttpClient client, ILogger<CompetitionService> logger)
    {
        _client = client;
        _logger = logger;
    }
    public string Baseaddress => _client.BaseAddress?.ToString();

    public async Task<IEnumerable<CompetitionResultDto>> GetMatchCompetitions(int matchId)
    {
        return await _client.GetFromJsonAsync<IEnumerable<CompetitionResultDto>>($"api/competitionresult/{matchId}");
    }
    public async Task<IEnumerable<ListItem>?> GetCompetitions()
    {
        var res = await _client.GetFromJsonAsync<IEnumerable<ListItem>>($"api/competition");
        return res;
    }

    public async Task<bool> UpsertGetCompetitionResult(CompetitionUpsertDto dto)
    {
        var res = await _client.PutAsJsonAsync<CompetitionUpsertDto>($"api/competitionresult", dto);
        return res.IsSuccessStatusCode;
    }
    public async Task DeleteCompetitionResult(int resultId)
    {
        await _client.DeleteAsync($"api/competitionresult/{resultId}");
    }
}

