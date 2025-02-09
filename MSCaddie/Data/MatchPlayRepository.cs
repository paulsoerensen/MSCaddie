using AutoMapper;
using Dapper;
using MSCaddie.Shared.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Dtos;


namespace MSCaddie.Data;

public class MatchPlayRepository : RepositoryBase, IMatchPlayRepository
{
    private int season;
    public MatchPlayRepository(IConfiguration config,
        IAdminRepository adminRepo,
        ILogger<MatchPlayRepository> logger,
        IMapper mapper) : base(config, logger, mapper)
    {
        season = adminRepo.Season;
    }
    #region LeagueTeam


    public async Task<IEnumerable<MatchPlayTeamDto?>?> MatchPlayTeamList(int leagueId)
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

    public async Task<IEnumerable<PlayerForMatchPlayDto>> GetPlayersForMatchPlay()
    {
        string sql = @"SELECT LeagueTeamId, VgcNo,Firstname,Lastname,LeagueId,Season
                        ,VgcNoPartner,Firstname2,Lastname2
                        from [ms].[fnGetPlayersForMatchPlay](@season)
                        order by Lastname";

        using IDbConnection db = new SqlConnection(ConnectionString);
        return await db.QueryAsync<PlayerForMatchPlayDto>(sql, new { season });
    }

    public async Task<IEnumerable<PlayerDto>> GetPlayersForMatchPlayPar()
    {
        string sql = @"SELECT VgcNo,Firstname,Lastname
                        FROM [ms].[fnGetMatchPlayPartner] (@season)
                        order by Lastname";

        using IDbConnection db = new SqlConnection(ConnectionString);
        return await db.QueryAsync<PlayerDto>(sql, new { season });
    }

    public async Task<IEnumerable<MatchPlayTeamDto>> GetMatchplays()
    {
        string sql = @"SELECT LeagueId, LeagueName, Playround, LeagueMatchId, 
                        MatchResult, ResultText, TeamName1, TeamName2, LeagueTeamId1, LeagueTeamId2 
                        from ms.vLeagueMatch 
                        where season = @season 
                        order by Playround desc, LastUpdate desc";

        using IDbConnection db = new SqlConnection(ConnectionString);
        return await db.QueryAsync<MatchPlayTeamDto>(sql, new { season });
    }

    public async Task DeleteMatchplayPar(int id)
    {
        string sql = @"delete ms.LeagueTeam where LeagueTeamId = @id";

        using IDbConnection db = new SqlConnection(ConnectionString);
        await db.ExecuteScalarAsync(sql, new { id });
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
        model.Season = model.Season < 2024 ? season : model.Season;
        using var con = new SqlConnection(ConnectionString);
        using var cmd = con.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "[ms].[LeagueTeamUpsert]";
        cmd.Parameters.AddWithValue("LeagueId", model.LeagueId);
        cmd.Parameters.AddWithValue("Season", model.Season);
        cmd.Parameters.AddWithValue("TeamName", model.TeamName);
        cmd.Parameters.AddWithValue("VgcNo", model.VgcNo1);
        cmd.Parameters.AddWithValue("VgcNoPartner", model.VgcNo2);  // Handle NULL VgcNoPartner

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



    //public int MatchPlayTeamUpdate(Dto.LeagueTeam team)
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
    //public int MatchPlayMatchUpdate(Dto.LeagueMatch m)
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

    //public int MatchPlayDeleteSeasonTeams(int season)
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
    //public IEnumerable<Dto.LeagueTeam> GetMatchplayTeams(int leagueId)
    //{
    //    int season = DateTime.Now.Year;
    //    MSDatabase.EnableAutoSelect = false;
    //    var list = MSDatabase.Query<Dto.LeagueTeam>("SELECT [LeagueTeamId],[TeamName]," +
    //                    " [VgcNo],[VgcNoPartner],[Season],[LeagueId] " +
    //                    " FROM [ms].[LeagueTeam] " +
    //                    " where [LeagueId] = @LeagueId and [Season] = @Season" +
    //                    " order by TeamName",
    //                     new { LeagueId = leagueId, Season = season });
    //    return list;
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

    //public List<KeyValuePair<int, String>> GetMatchPlayLeagueList()
    //{
    //    return new List<KeyValuePair<int, String>>()
    //    {
    //        new KeyValuePair<int, String>(1, "Single A"),
    //        new KeyValuePair<int, String>(2, "Single B"),
    //        new KeyValuePair<int, String>(3, "Par")
    //    };
    //}
}
