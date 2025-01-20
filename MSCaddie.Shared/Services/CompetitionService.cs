using Microsoft.Extensions.Logging;
using MSCaddie.Shared.Interfaces;
using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Services;
public class CompetitionService : ICompetitionService
{
    ILogger<CompetitionService> _logger;
    private readonly IMatchRepository _repo;

    public CompetitionService(IMatchRepository repo, ILogger<CompetitionService> logger)
    {
        _logger = logger;
        _repo = repo;
    }
    public async Task<IEnumerable<CompetitionResult>> GetMatchCompetitions(int id)
    {
        return await _repo.GetCompetitionResults(id);
        //return await _client.GetFromJsonAsync<IEnumerable<CompetitionResultDto>>($"api/competitionresult/{matchId}");
    }
    public async Task<IEnumerable<ListEntry>?> GetCompetitions()
    {
        return await _repo.GetCompetitions();
        //var res = await _client.GetFromJsonAsync<IEnumerable<ListItem>>($"api/competition");
        //return res;
    }

    public async Task<bool> UpsertGetCompetitionResult(CompetitionUpsert model)
    {
        int i = await _repo.UpsertCompetitionResult(model);
        return i > 0;
        //var res = await _client.PutAsJsonAsync<CompetitionUpsert>($"api/competitionresult", dto);
        //return res.IsSuccessStatusCode;
    }
    public async Task<bool> DeleteCompetitionResult(int id)
    {
        int i = await _repo.DeleteCompetitionResult(id);
        return i > 0;
        //await _client.DeleteAsync($"api/competitionresult/{resultId}");
    }
}

