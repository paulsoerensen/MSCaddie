using AutoMapper;
using Dapper;
using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MSCaddie.Data;


public class MatchPlayRespository : RepositoryBase, IMatchPlayRespository
{
    public MatchPlayRespository(IConfiguration config, ILogger<MatchPlayRespository> logger, IMapper mapper) : base(config, logger, mapper)
    {
    }

    #region LeagueTeam
    public async Task<IEnumerable<MatchPlayTeamDto?>> GetLeagueTeams(int season, int leagueId)
    {
        try
        {
            string sql = "exec [ms].[LeagueTeamSelectAll] @Season, @LeagueId";
            using IDbConnection db = new SqlConnection(ConnectionString);
            return await db.QueryAsync<MatchPlayTeamDto>(sql, new { Season = season, LeagueId = leagueId });
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<MatchPlayTeamDto?> GetLeagueTeam(int leagueTeamId)
    {
        string sql = @"SELECT [LeagueTeamId], [LeagueId], [Season], [TeamName], [VgcNo], [VgcNoPartner]
                       FROM ms.LeagueTeam
                       WHERE [LeagueTeamId] = @LeagueTeamId";

        using (IDbConnection db = new SqlConnection(ConnectionString))
            return await db.QueryFirstOrDefaultAsync<MatchPlayTeamDto>(sql, new { LeagueTeamId = leagueTeamId });
    }

    public async Task<MatchPlayTeamDto> LeagueTeamUpsert(MatchPlayTeamDto model)
    {
        using var con = new SqlConnection(ConnectionString);
        using var cmd = con.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "[ms].[LeagueTeamUpsert]";
        cmd.Parameters.AddWithValue("LeagueId", model.LeagueId);
        cmd.Parameters.AddWithValue("Season", model.Season);
        cmd.Parameters.AddWithValue("TeamName", model.TeamName);
        cmd.Parameters.AddWithValue("VgcNo", model.VgcNo);
        cmd.Parameters.AddWithValue("VgcNoPartner", model.VgcNoPartner ?? (object)DBNull.Value);  // Handle NULL VgcNoPartner

        cmd.CommandTimeout = 240;
        con.Open();
        await cmd.ExecuteNonQueryAsync();

        return model;
    }

    public async Task<bool> DeleteLeagueTeam(int leagueTeamId)
    {
        try
        {
            string sql = "DELETE FROM ms.LeagueTeam WHERE [LeagueTeamId] = @LeagueTeamId";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var result = await db.ExecuteAsync(sql, new { LeagueTeamId = leagueTeamId });
                return result > 0;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return false;
        }
    }
    #endregion
}
