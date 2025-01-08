using MSCaddie.Shared.Dtos;

namespace MSCaddie.Shared.Services
{
    public interface ICompetitionService
    {
        string Baseaddress { get; }

        Task<IEnumerable<CompetitionResultDto>> GetMatchCompetitions(int matchId);
        Task<IEnumerable<ListItem>?> GetCompetitions();
        Task<bool> UpsertGetCompetitionResult(CompetitionUpsertDto dto);
        Task DeleteCompetitionResult(int resultId);
    }
}