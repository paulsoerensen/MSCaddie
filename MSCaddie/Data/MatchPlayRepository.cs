using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using MSCaddie.Shared.Dtos;
using MSCaddie.Shared.Interfaces;
using System.Data;


namespace MSCaddie.Data;

public class MatchplayRepository : RepositoryBase, IMatchplayRepository
{
    private int season;
    IPlayerRepository playerRepo;
   public MatchplayRepository(IConfiguration config,
        IAdminRepository adminRepo,
        IPlayerRepository plRepo,
        ILogger<MatchplayRepository> logger,
        IMapper mapper) : base(config, logger, mapper)
    {
        season = adminRepo.Season;
        playerRepo = plRepo;
    }

    #region Matchplay teams, single
    /// <summary>
    /// Teams and potential single teams
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<TeamSingleDto>> GetMatchplayTeams()
    {
        string sql = @"select Season, Firstname, Lastname, VgcNo, TeamSingleId, TeamName, League 
                    FROM ms.vTeamSingle
                    WHERE [season] = @season
                    order by LastName";

        using (IDbConnection db = new SqlConnection(ConnectionString))
            return await db.QueryAsync<TeamSingleDto>(sql, new { season });
    }

    public async Task<int> MatchplayTeamUpsert(TeamSingleDto model)
    {
        model.Season = model.Season < 2024 ? season : model.Season;
        using var con = new SqlConnection(ConnectionString);
        using var cmd = con.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "[ms].[TeamSingleUpsert]";
        cmd.Parameters.AddWithValue("TeamSingleId", model.TeamSingleId);
        cmd.Parameters.AddWithValue("League", model.League);
        cmd.Parameters.AddWithValue("Season", model.Season);
        cmd.Parameters.AddWithValue("TeamName", model.TeamName);
        cmd.Parameters.AddWithValue("VgcNo", model.VgcNo);

        cmd.CommandTimeout = 240;
        con.Open();
        return await cmd.ExecuteNonQueryAsync();
    }

    public async Task<int> MatchplayTeamDelete(int id)
    {
        string sql = @"delete ms.TeamSingle where TeamSingleId = @id";

        using IDbConnection db = new SqlConnection(ConnectionString);
        var res = await db.ExecuteScalarAsync(sql, new { id });
        return Convert.ToInt32(res ?? 0);
    }
    #endregion

    #region Matchplay teams, par

    /// <summary>
    /// Players without a partner
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<PlayerDto>> GetTeamPartners()
    {
        string sql = @"SELECT [PlayerId],[VgcNo],[Firstname],[Lastname],[Season] 
                    from ms.vTeamPartner
                    WHERE [season] = @season
                    Order by Lastname";

        using (IDbConnection db = new SqlConnection(ConnectionString))
            return await db.QueryAsync<PlayerDto>(sql, new { season });
    }

    /// <summary>
    /// Get par Teams
    /// </summary>
    /// <returns>Par Teams</returns>
    public async Task<IEnumerable<TeamParDto>> GetMatchplayTeamPars()
    {
        string sql = @"select Season, VgcNo, VgcNoPartner, TeamParId, TeamName
                    FROM ms.TeamPar
                    WHERE [season] = @season
                    Order by TeamName";

        using (IDbConnection db = new SqlConnection(ConnectionString))
            return await db.QueryAsync<TeamParDto>(sql, new { season });
    }

    public async Task<int> MatchplayTeamParUpsert(TeamParDto model)
    {
        model.Season = model.Season < 2024 ? season : model.Season;
        using var con = new SqlConnection(ConnectionString);
        using var cmd = con.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "[ms].[TeamParUpsert]";
        cmd.Parameters.AddWithValue("TeamParId", model.TeamParId);
        cmd.Parameters.AddWithValue("Season", model.Season);
        cmd.Parameters.AddWithValue("TeamName", model.TeamName);
        cmd.Parameters.AddWithValue("VgcNo", model.VgcNo);
        cmd.Parameters.AddWithValue("VgcNoPartner", model.VgcNoPartner);

        cmd.CommandTimeout = 240;
        con.Open();
        return await cmd.ExecuteNonQueryAsync();
    }

    public async Task<int> MatchplayTeamParDelete(int id)
    {
        string sql = @"delete ms.TeamPar where TeamParId = @id";

        using IDbConnection db = new SqlConnection(ConnectionString);
        var res = await db.ExecuteScalarAsync(sql, new { id });
        return Convert.ToInt32(res ?? 0);
    }
    #endregion

    #region Match fixing, single
    /// <summary>
    /// Teams for match fixing
    /// </summary>
    /// <param name="leagueId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<MatchplayTeamDto>> GetMatchplayTeams(char league)
    {
        try { 
            string sql;
            if (league.Equals('A') || league.Equals('B'))
            {
                sql = @"select Season, TeamSingleId as TeamId, TeamName, League 
                        FROM ms.TeamSingle
                        WHERE [season] = @season AND League = @league
                        Order by TeamName";
                using (IDbConnection db = new SqlConnection(ConnectionString))
                    return await db.QueryAsync<MatchplayTeamDto>(sql, new { season, league });
            }
            else
            {
                sql = @"select Season, TeamParId as TeamId, TeamName 
                        FROM ms.TeamPar
                        WHERE [season] = @season
                        Order by TeamName";
                using (IDbConnection db = new SqlConnection(ConnectionString))
                    return await db.QueryAsync<MatchplayTeamDto>(sql, new { season });
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<IEnumerable<MatchplayGameDto>> GetMatchplayGames(char league)
    {
        string sql = @"SELECT MatchplayGameId, MatchResult, ResultText, League, 
                        PlayRound, TeamId1, TeamName1, TeamId2, TeamName2, LastUpdate
                        FROM [ms].[vMatchplayGame]
                        WHERE League = @league and Season = @season
                        order by PlayRound desc, LastUpdate desc";
        using (IDbConnection db = new SqlConnection(ConnectionString))
            return await db.QueryAsync<MatchplayGameDto>(sql, new { league, season });
    }

    public async Task<int> MatchplayGameUpsert(MatchplayGameDto model)
    {
        using var con = new SqlConnection(ConnectionString);
        using var cmd = con.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "[ms].[MatchplayGameUpsert]";
        cmd.Parameters.AddWithValue("@MatchplayGameId", model.MatchplayGameId);
        cmd.Parameters.AddWithValue("MatchResult", model.MatchResult);
        cmd.Parameters.AddWithValue("ResultText", model.ResultText);
        cmd.Parameters.AddWithValue("Season", season);
        cmd.Parameters.AddWithValue("League", model.League);
        cmd.Parameters.AddWithValue("PlayRound", model.PlayRound);
        cmd.Parameters.AddWithValue("TeamId1", model.TeamId1);
        cmd.Parameters.AddWithValue("TeamId2", model.TeamId2);

        cmd.CommandTimeout = 240;
        con.Open();
        return await cmd.ExecuteNonQueryAsync();
    }

    
    public async Task<int> MatchplayGameDelete(int id)
    {
        string sql = @"delete ms.MatchplayGame where MatchplayGameId = @id";

        using IDbConnection db = new SqlConnection(ConnectionString);
        var res = await db.ExecuteScalarAsync(sql, new { id });
        return Convert.ToInt32(res ?? 0);
    }


    #endregion

    #region Match fixing, par

    public async Task<IEnumerable<MatchplayGameDto>> GetMatchplayGamePars(char league)
    {
        string sql = @"SELECT MatchplayGameId, MatchResult, ResultText, League, 
                        PlayRound, TeamId1, TeamName1, TeamId2, TeamName2, LastUpdate
                        FROM [ms].[vMatchplayGame]
                        WHERE League = @league and Season = @season
                        order by PlayRound desc, LastUpdate desc";
        using (IDbConnection db = new SqlConnection(ConnectionString))
            return await db.QueryAsync<MatchplayGameDto>(sql, new { league, season });
    }

    public async Task<int> MatchplayGameParUpsert(MatchplayGameDto model)
    {
        using var con = new SqlConnection(ConnectionString);
        using var cmd = con.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "[ms].[MatchplayGameUpsert]";
        cmd.Parameters.AddWithValue("@MatchplayGameId", model.MatchplayGameId);
        cmd.Parameters.AddWithValue("MatchResult", model.MatchResult);
        cmd.Parameters.AddWithValue("ResultText", model.ResultText);
        cmd.Parameters.AddWithValue("Season", season);
        cmd.Parameters.AddWithValue("League", model.League);
        cmd.Parameters.AddWithValue("PlayRound", model.PlayRound);
        cmd.Parameters.AddWithValue("TeamId1", model.TeamId1);
        cmd.Parameters.AddWithValue("TeamId2", model.TeamId2);

        cmd.CommandTimeout = 240;
        con.Open();
        return await cmd.ExecuteNonQueryAsync();
    }


    public async Task<int> MatchplayGameParDelete(int id)
    {
        string sql = @"delete ms.MatchplayGame where MatchplayGameId = @id";

        using IDbConnection db = new SqlConnection(ConnectionString);
        var res = await db.ExecuteScalarAsync(sql, new { id });
        return Convert.ToInt32(res ?? 0);
    }
    #endregion


    #region


    public async Task<IEnumerable<MatchplayTeamDto?>?> MatchplayTeamList(int leagueId)
    {
        try
        {
            string sql = "exec [ms].[LeagueTeamSelectAll] @Season, @LeagueId";
            using IDbConnection db = new SqlConnection(ConnectionString);
            return await db.QueryAsync<MatchplayTeamDto>(sql, new { Season = season, LeagueId = leagueId });
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<IEnumerable<PlayerForMatchplayDto>> GetPlayersForMatchplay()
    {
        string sql = @"SELECT LeagueTeamId, VgcNo,Firstname,Lastname,LeagueId,Season
                        ,VgcNoPartner,Firstname2,Lastname2
                        from [ms].[fnGetPlayersForMatchplay](@season)
                        order by Lastname";

        using IDbConnection db = new SqlConnection(ConnectionString);
        return await db.QueryAsync<PlayerForMatchplayDto>(sql, new { season });
    }

    public async Task<IEnumerable<PlayerDto>> GetPlayersForMatchplayPar()
    {
        string sql = @"SELECT VgcNo,Firstname,Lastname
                        FROM [ms].[fnGetMatchplayPartner] (@season)
                        order by Lastname";

        using IDbConnection db = new SqlConnection(ConnectionString);
        return await db.QueryAsync<PlayerDto>(sql, new { season });
    }

    public async Task<IEnumerable<MatchplayTeamDto>> GetMatchplays()
    {
        string sql = @"SELECT LeagueId, LeagueName, Playround, LeagueMatchId, 
                        MatchResult, ResultText, TeamName1, TeamName2, LeagueTeamId1, LeagueTeamId2 
                        from ms.vLeagueMatch 
                        where season = @season 
                        order by Playround desc, LastUpdate desc";

        using IDbConnection db = new SqlConnection(ConnectionString);
        return await db.QueryAsync<MatchplayTeamDto>(sql, new { season });
    }

    public async Task DeleteMatchplayPar(int id)
    {
        string sql = @"delete ms.LeagueTeam where LeagueTeamId = @id";

        using IDbConnection db = new SqlConnection(ConnectionString);
        await db.ExecuteScalarAsync(sql, new { id });
    }

    public async Task<MatchplayTeamDto?> GetLeagueTeam(int leagueTeamId)
    {
        string sql = @"SELECT [LeagueTeamId], [LeagueId], [Season], [TeamName], [VgcNo], [VgcNoPartner]
                    FROM ms.LeagueTeam
                    WHERE [LeagueTeamId] = @LeagueTeamId";

        using (IDbConnection db = new SqlConnection(ConnectionString))
            return await db.QueryFirstOrDefaultAsync<MatchplayTeamDto>(sql, new { LeagueTeamId = leagueTeamId });
    }

    //public async Task<MatchplayTeamDto> LeagueTeamUpsert(MatchplayTeamDto model)
    //{
    //    model.Season = model.Season < 2024 ? season : model.Season;
    //    using var con = new SqlConnection(ConnectionString);
    //    using var cmd = con.CreateCommand();
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.CommandText = "[ms].[LeagueTeamUpsert]";
    //    cmd.Parameters.AddWithValue("LeagueId", model.LeagueId);
    //    cmd.Parameters.AddWithValue("Season", model.Season);
    //    cmd.Parameters.AddWithValue("TeamName", model.TeamName);
    //    cmd.Parameters.AddWithValue("VgcNo", model.VgcNo1);
    //    cmd.Parameters.AddWithValue("VgcNoPartner", model.VgcNo2);  // Handle NULL VgcNoPartner

    //    cmd.CommandTimeout = 240;
    //    con.Open();
    //    await cmd.ExecuteNonQueryAsync();

    //    return model;
    //}

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


    public async Task<IEnumerable<MatchTeamDto>> GetMatchTeams(int leagueId)
    {
        string sql = @"SELECT t.LeagueTeamId, t.TeamName, COALESCE(MAX(m.PlayRound), 0) AS PlayRound 
                        FROM ms.LeagueTeam AS t 
                            LEFT JOIN ms.LeagueMatch AS m 
                                ON t.LeagueTeamId = m.LeagueTeamId1
                         WHERE t.[LeagueId] = @LeagueId and [season] = @season
                        GROUP BY t.LeagueTeamId, t.TeamName";

        using (IDbConnection db = new SqlConnection(ConnectionString))
        return await db.QueryAsync<MatchTeamDto>(sql, new { leagueId, season });
    }






    #endregion



    //public int MatchplayTeamUpdate(Dto.LeagueTeam team)
    //{
    //    MSDatabase.EnableAutoSelect = false;
    //    if (team.IsSingle)
    //    {
    //        var result = MSDatabase.Execute(";exec [ms].[LeagueTeamUpsert] @LeagueId,  @VgcNo"
    //                , new { LeagueId = team.LeagueId, VgcNo = team.VgcNo });
    //        return result;
    //    }
    //    else
    //    {
    //        var result = MSDatabase.Execute(";exec [ms].[LeagueTeamDoubleUpsert] @LeagueId, @VgcNo, @VgcNoPartner"
    //                , new { LeagueId = team.LeagueId, VgcNo = team.VgcNo, VgcNoPartner = team.VgcNoPartner });
    //        return result;
    //    }
    //}
    //public int MatchplayMatchUpdate(Dto.LeagueMatch m)
    //{
    //    MSDatabase.EnableAutoSelect = false;
    //    var result = MSDatabase.Execute(";EXECUTE [ms].[MatchplayMatchUpsert] " +
    //            "@LeagueMatchId, @LeagueId, @Playround, @LeagueTeamId1, @LeagueTeamId2;",
    //            new
    //            {
    //                @LeagueMatchId = m.LeagueMatchId,
    //                @LeagueId = m.LeagueId,
    //                @Playround = m.Playround,
    //                @LeagueTeamId1 = m.LeagueTeamId1,
    //                @LeagueTeamId2 = m.LeagueTeamId2
    //            });
    //    return result;
    //}

    //public int MatchplayDeleteSeasonTeams(int season)
    //{
    //    MSDatabase.EnableAutoSelect = false;
    //    return MSDatabase.Execute("delete [ms].[LeagueTeam] where Season = @Season",
    //            new { Season = season });
    //}
    //    public async Task<LeagueMatch>GetMatchplay(int matchId)
    //{
    //    string sql = @"SELECT LeagueId, LeagueName, Playround, LeagueMatchId, 
    //                       MatchResult, ResultText, TeamName1, TeamName2, LeagueTeamId1, LeagueTeamId2 
    //                       from ms.vLeagueMatch 
    //                       where LeagueMatchId = @matchId";

    //    using IDbConnection db = new SqlConnection(ConnectionString);
    //    var res = await db.QueryAsync<LeagueMatch>(sql, new { matchId = matchId });

    //    return res.FirstOrDefault();
    //}



    //public int UpdateMatchplayResult(Dto.LeagueMatch dto)
    //{
    //    MSDatabase.EnableAutoSelect = false;
    //    var result = MSDatabase.Execute(";UPDATE [ms].[LeagueMatch]" +
    //        " SET [MatchResult] = @matchResult,[ResultText] = @resultText, [LastUpdate] = Getdate()" +
    //        " WHERE LeagueMatchId = @matchId"
    //        , new
    //        {
    //            matchId = dto.LeagueMatchId,
    //            matchResult = dto.MatchResult,
    //            resultText = dto.ResultText
    //        });
    //    return result;
    //}


    //public Dto.LeagueTeam MatchplayTeamExists(int vgcNo)
    //{
    //    int season = DateTime.Now.Year;
    //    MSDatabase.EnableAutoSelect = false;
    //    var list = MSDatabase.Query<Dto.LeagueTeam>("SELECT [LeagueTeamId],[TeamName]," +
    //                    " [VgcNo],[VgcNoPartner],[Season],[LeagueId] " +
    //                    " FROM [ms].[LeagueTeam] " +
    //                    " where [LeagueId] < 3 and [Season] = @Season and [VgcNo] = @VgcNo" +
    //                    " order by TeamName",
    //                     new { VgcNo = vgcNo, Season = season });
    //    return list.FirstOrDefault();
    //}

    //public Dto.LeagueTeam MatchplayTeamExists(int vgcNo, int vgcNoPartner)
    //{
    //    int season = DateTime.Now.Year;
    //    if (vgcNo > vgcNoPartner)
    //    {
    //        int i = vgcNo;
    //        vgcNo = vgcNoPartner;
    //        vgcNoPartner = i;
    //    }

    //    MSDatabase.EnableAutoSelect = false;
    //    var list = MSDatabase.Query<Dto.LeagueTeam>("SELECT [LeagueTeamId],[TeamName]," +
    //                    " [VgcNo],[VgcNoPartner],[Season],[LeagueId] " +
    //                    " FROM [ms].[LeagueTeam] " +
    //                    " where [LeagueId] = 3 and [Season] = @Season and [VgcNo] = @VgcNo" +
    //                    " and [VgcNoPartner] = @VgcNoPartner" +
    //                    " order by TeamName",
    //                     new { VgcNo = vgcNo, VgcNoPartner = vgcNoPartner, Season = season });
    //    return list.FirstOrDefault();
    //}
    //public Dto.LeagueTeam MatchplayGetTeam(int id)
    //{
    //    MSDatabase.EnableAutoSelect = false;
    //    var list = MSDatabase.Query<Dto.LeagueTeam>("SELECT [LeagueTeamId],[TeamName]," +
    //                    " [VgcNo],[VgcNoPartner],[Season],[LeagueId] " +
    //                    " FROM [ms].[LeagueTeam] " +
    //                    " where [LeagueTeamId] = @Id",
    //                     new { Id = id });
    //    return list.FirstOrDefault();
    //}
    //public int MatchplayDeleteTeam(int id)
    //{
    //    MSDatabase.EnableAutoSelect = false;
    //    var res = MSDatabase.Execute(";delete [ms].[LeagueTeam] " +
    //                    " where [LeagueTeamId] = @Id",
    //                     new { Id = id });
    //    return 0;
    //}

    //public List<KeyValuePair<int, String>> GetMatchplayLeagueList()
    //{
    //    return new List<KeyValuePair<int, String>>()
    //    {
    //        new KeyValuePair<int, String>(1, "Single A"),
    //        new KeyValuePair<int, String>(2, "Single B"),
    //        new KeyValuePair<int, String>(3, "Par")
    //    };
    //}
}
