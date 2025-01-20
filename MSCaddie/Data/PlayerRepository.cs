using AutoMapper;
using Dapper;
using   MSCaddie.Shared.Models;
using  MSCaddie.Shared.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;


namespace MSCaddie.Data
{
    public class PlayerRepository : RepositoryBase, IPlayerRepository
    {
        public PlayerRepository(IConfiguration config, ILogger<PlayerRepository> logger, IMapper mapper) : base(config, logger, mapper)
        {
            ;
        }

        #region Player
        public async Task<IEnumerable<Player?>> GetPlayers(int season)
        {
            try
            {
                string sql = "exec [ms].[PlayerSelectAll] @Season";
                using IDbConnection db = new SqlConnection(ConnectionString);
                var res = await db.QueryAsync<Player>(sql, new { Season=season });
                return res.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }
        }
        public async Task<Player?> GetPlayer(int vgcNo)
        {
            string sql = @"SELECT Top(1) [VgcNo],[FirstName],[LastName],[ZipCode],[City],[Address],[Email]," +
                "[Sponsor],[Phone],[CellPhone],[HcpIndex],[HcpUpdated]," +
                "[LastUpdate],[PlayerId] " +
                "FROM ms.Player where [vgcNo]=@vgcNo";

            using (IDbConnection db = new SqlConnection(ConnectionString))
                return (Player?)(await db.QueryAsync<Player>(sql, new { vgcNo })).FirstOrDefault();
        }

        public async Task<Player> PlayerUpsert(Player model)
        {
            using var con = new SqlConnection(ConnectionString);

            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[PlayerUpsert]";
            cmd.Parameters.AddWithValue("vgcNo", model.@VgcNo);
            cmd.Parameters.AddWithValue("Firstname", model.Firstname);
            cmd.Parameters.AddWithValue("Lastname", model.Lastname);
            cmd.Parameters.AddWithValue("ZipCode", model.ZipCode);
            cmd.Parameters.AddWithValue("City", model.City);
            cmd.Parameters.AddWithValue("Address", model.Address);
            cmd.Parameters.AddWithValue("Email", model.Email);
            cmd.Parameters.AddWithValue("Sponsor", model.Sponsor);
            cmd.Parameters.AddWithValue("Phone", model.Phone);
            cmd.Parameters.AddWithValue("HcpIndex", model.HcpIndex);

            cmd.CommandTimeout = 240;
            con.Open();
            await cmd.ExecuteNonQueryAsync();

            return model;
        }

        #endregion
    }
}