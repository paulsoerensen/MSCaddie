using AutoMapper;
using Dapper;
using MSCaddie.Shared.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;
using MSCaddie.Shared.Models;


namespace MSCaddie.Data
{
    public class MatchPlayRepository : RepositoryBase, IMatchPlayRepository
    {
        public MatchPlayRepository(IConfiguration config, ILogger<PlayerRepository> logger, IMapper mapper) : base(config, logger, mapper)
        {
        }
        public async Task<IEnumerable<LeagueTeam>>MatchPlayTeamList()
        {
            int season = DateTime.Now.Year;
            string sql = "SELECT [LeagueId],[LeagueName],[LeagueTeamId],[TeamName],[Season],[VgcNo],[VgcNoPartner] " +
                                "FROM [ms].[vLeagueTeam] where Season = @Season order by VgcNo";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return await db.QueryAsync<LeagueTeam>(sql, new { Season = season });
        }

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
        public async Task<LeagueMatch>GetMatchplay(int matchId)
        {
            string sql = @"SELECT LeagueId, LeagueName, Playround, LeagueMatchId, 
                               MatchResult, ResultText, TeamName1, TeamName2, LeagueTeamId1, LeagueTeamId2 
                               from ms.vLeagueMatch 
                               where LeagueMatchId = @matchId";

            using IDbConnection db = new SqlConnection(ConnectionString);
            var res = await db.QueryAsync<LeagueMatch>(sql, new { matchId = matchId });

            return res.FirstOrDefault();
        }

        public async Task<IEnumerable<LeagueMatch>> GetMatchplays()
        {
            int season = DateTime.Now.Year;
            string sql = @"SELECT LeagueId, LeagueName, Playround, LeagueMatchId, 
                            MatchResult, ResultText, TeamName1, TeamName2, LeagueTeamId1, LeagueTeamId2 
                            from ms.vLeagueMatch 
                            where season = @season 
                            order by Playround desc, LastUpdate desc";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return await db.QueryAsync<LeagueMatch>(sql, new { season = season });
        }

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
}