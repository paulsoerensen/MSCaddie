using AutoMapper;
using Dapper;
using MSCaddie.Shared.Dtos;
using System.Data;
using Microsoft.Data.SqlClient;
using MSCaddie.Shared.Interfaces;
using MSCaddie.Shared.Models;
using Radzen.Blazor.Rendering;


namespace MSCaddie.Data;


public class TourRepository : RepositoryBase, ITourRepository
{
    public TourRepository(IConfiguration config, ILogger<TourRepository> logger, IMapper mapper) : base(config, logger, mapper)
    {
        ;
    }

    #region Tour
    public async Task<IEnumerable<TourDto?>> GetTours(int season)
    {
        try
        {
            string sql = @"SELECT [TourId],[TourDate],[Description],[LastRegistrationDate],[OpenForSignUp],[MaxNoOfMembers]
                           [UrlDescription],[NoOfMembers],[MatchId],[SponsorLogoId],[UrlRegistration]
                           FROM [ms].[Tour] Where DATEPART(year, TourDate) = @Season";
            using IDbConnection db = new SqlConnection(ConnectionString);
            return await db.QueryAsync<TourDto>(sql, new { Season=season });
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<IEnumerable<TourPlayerDto?>> GetTourPlayers(int tourId)
    {
        try
        {

            string sql = @"with p as (
	                            SELECT t.TourId, t.[VgcNo],[SignedUp],[LastUpdateBy],
		                            t.[LastUpdate],Firstname, LastName
                                FROM [ms].[vMembers] as p
		                            left join [ms].[TourPlayer] as t
			                            on p.VgcNo = t.VgcNo
                            )
                            SELECT t.[TourId],[VgcNo],[SignedUp],[LastUpdateBy],
		                            [LastUpdate],Firstname, LastName
                            FROM[ms].[Tour] as t
			                    left join p
									on p.TourId = t.TourId
                            where t.TourId = @TourId
                            ORDER BY SignedUp, LastName";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return await db.QueryAsync<TourPlayerDto>(sql, new { TourId = tourId });
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return null;
        }
    }
    public async Task<IEnumerable<TourPlayerDto?>> GetNonTourPlayers(int tourId)
    {
        try
        {
            string sql = @"SELECT VgcNo, Firstname, LastName
                        FROM [ms].[PlayerNotInTour] (@tourId)";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return await db.QueryAsync<TourPlayerDto>(sql, new { tourId });
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<int> Subscribe(int tourId, int vgcNo)
    {
        string sql = @"UPDATE [ms].[TourPlayer]
                        SET [SignedUp] = CASE WHEN [SignedUp] = 1 THEN 0 ELSE 1 END
                            ,[LastUpdate] = sysdatetime()
                        WHERE [TourId] = @tourId and [VgcNo] = @vgcNo";

        using IDbConnection db = new SqlConnection(ConnectionString);
        var res = await db.ExecuteScalarAsync(sql, new { tourId, vgcNo });
        return Convert.ToInt32(res ?? 0);
    }

    public async Task<int>ToggleSubscribtion(int tourId, int vgcNo)
    {
        string sql = @"UPDATE [ms].[TourPlayer]
                        SET [SignedUp] = CASE WHEN [SignedUp] = 1 THEN 0 ELSE 1 END
                            ,[LastUpdate] = sysdatetime()
                        WHERE [TourId] = @tourId and [VgcNo] = @vgcNo";

        using IDbConnection db = new SqlConnection(ConnectionString);
        var res = await db.ExecuteScalarAsync(sql, new { tourId, vgcNo });
        return Convert.ToInt32(res ?? 0);
    }


    public async Task<TourDto> TourUpsert(TourDto dto)
    {
        using var con = new SqlConnection(ConnectionString);

        using var cmd = con.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "[ms].[TourUpsert]"; // Replace with your actual stored procedure name

        cmd.Parameters.AddWithValue("TourId", dto.TourId);
        cmd.Parameters.AddWithValue("TourDate", dto.TourDate);
        cmd.Parameters.AddWithValue("Description", (object)dto.Description ?? DBNull.Value);
        cmd.Parameters.AddWithValue("LastRegistrationDate", (object)dto.LastRegistrationDate ?? DBNull.Value);
        cmd.Parameters.AddWithValue("OpenForSignUp", (object)dto.OpenForSignUp ?? DBNull.Value);
        cmd.Parameters.AddWithValue("MaxNoOfMembers", (object)dto.MaxNoOfMembers ?? DBNull.Value);
        cmd.Parameters.AddWithValue("UrlDescription", dto.UrlDescription);
        cmd.Parameters.AddWithValue("NoOfMembers", dto.NoOfMembers);
        cmd.Parameters.AddWithValue("MatchId", (object)dto.MatchId ?? DBNull.Value);
        cmd.Parameters.AddWithValue("SponsorLogoId", dto.SponsorLogoId);
        cmd.Parameters.AddWithValue("UrlRegistration", (object)dto.UrlRegistration ?? DBNull.Value);

        cmd.CommandTimeout = 240;
        con.Open();
        await cmd.ExecuteNonQueryAsync();

        return dto;
    }

    public async Task<TourPlayerDto> TourPlayerUpsert(TourPlayerDto dto)
    {
        using var con = new SqlConnection(ConnectionString);

        using var cmd = con.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "[ms].[TourPlayerUpsert]"; // Replace with your actual stored procedure name

        cmd.Parameters.AddWithValue("TourId", dto.TourId);
        cmd.Parameters.AddWithValue("VgcNo", dto.VgcNo);
        cmd.Parameters.AddWithValue("SignedUp", dto.SignedUp);
        cmd.Parameters.AddWithValue("LastUpdateBy", dto.LastUpdateBy);

        cmd.CommandTimeout = 240;
        con.Open();
        await cmd.ExecuteNonQueryAsync();

        return dto;
    }
    #endregion
}