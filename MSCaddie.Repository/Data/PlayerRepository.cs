using AutoMapper;
using Dapper;
using MSCaddie.Repository.Dtos;
using MSCaddie.Repository.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MSCaddie.Repository.Models;


namespace MSCaddie.Repository.Data;

public class PlayerRepository : RepositoryBase, IPlayerRepository
{
    public PlayerRepository(IConfiguration config, ILogger<PlayerRepository> logger, IMapper mapper) : base(config, logger, mapper)
    {
        ;
    }

    #region Player
    public async Task<IEnumerable<PlayerModel?>> GetPlayers(int season)
    {
        try
        {
            string sql = "exec [ms].[PlayerSelectAll] @Season";
            using IDbConnection db = new SqlConnection(ConnectionString);
            return await db.QueryAsync<PlayerModel>(sql, new { Season=season });
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return null;
        }
    }
    public async Task<IEnumerable<PlayerModel?>?> GetNonMembers(int season)
    {
        var res = await GetPlayers(season);
        return res?.Where(player => player?.Season == 0);
    }
    public async Task<PlayerModel?> GetPlayer(int vgcNo)
    {
        string sql = @"SELECT Top(1) [VgcNo],[FirstName],[LastName],[ZipCode],[City],[Address],[Email]," +
            "[Sponsor],[Phone],[CellPhone],[HcpIndex],[HcpUpdated]," +
            "[LastUpdate],[PlayerId] " +
            "FROM ms.Player where [vgcNo]=@vgcNo";

        using (IDbConnection db = new SqlConnection(ConnectionString))
            return (PlayerModel?)(await db.QueryAsync<PlayerModel>(sql, new { vgcNo })).FirstOrDefault();
    }

    public async Task<PlayerModel> PlayerUpsert(PlayerModel model)
    {
        using var con = new SqlConnection(ConnectionString);

        using var cmd = con.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "[ms].[PlayerUpsert]";
        cmd.Parameters.AddWithValue("vgcNo", model.@VgcNo);
        cmd.Parameters.AddWithValue("Firstname", model.Firstname);
        cmd.Parameters.AddWithValue("Lastname", model.Lastname);
        cmd.Parameters.AddWithValue("Email", model.Email);
        cmd.Parameters.AddWithValue("Phone", model.Phone);
        cmd.Parameters.AddWithValue("HcpIndex", model.HcpIndex);

        cmd.CommandTimeout = 240;
        con.Open();
        await cmd.ExecuteNonQueryAsync();

        return model;
    }

    #endregion
    #region Members
    public async Task<MembershipDto> MembershipUpsert(PlayerModel model)
    {
        using var con = new SqlConnection(ConnectionString);

        using var cmd = con.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "[ms].[MembershipUpsert]";
        //cmd.Parameters.AddWithValue("MemberShipId", model.MemberShipId);
        cmd.Parameters.AddWithValue("vgcNo", model.@VgcNo);
        cmd.Parameters.AddWithValue("Season", model.Season);

        var memberShipIdParam = new SqlParameter("MemberShipId", SqlDbType.Int)
        {
            Direction = ParameterDirection.InputOutput,
            Value = model.MemberShipId
        };
        cmd.Parameters.Add(memberShipIdParam);

        cmd.CommandTimeout = 240;
        await con.OpenAsync();
        await cmd.ExecuteNonQueryAsync();

        var membershipId = (int)memberShipIdParam.Value;

        return new MembershipDto
        {
            MembershipId = membershipId,
            VgcNo = model.VgcNo,
            Season = model.Season
        };
    }
    #endregion

}