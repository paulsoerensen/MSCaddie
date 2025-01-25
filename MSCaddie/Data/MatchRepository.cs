using AutoMapper;
using Dapper;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MSCaddie.Data
{
    public class MatchRepository : RepositoryBase, IMatchRepository
    {
        public MatchRepository(IConfiguration config, ILogger<PlayerRepository> logger, IMapper mapper) : base(config, logger, mapper)
        {
        }

        #region Method: MatchResults

        public async Task<IEnumerable<ListEntry>>GetMatchResultDates(DateTime seasonStart)
        {
            return await GetMatchResultDates(seasonStart, seasonStart);
        }

        public  async Task<IEnumerable<ListEntry>> GetMatchResultDates(DateTime startDate, DateTime endDate)
        {
            string sql = "exec [ms].[MatchResultSelectDates] @StartDate=startDate, @EndDate= endDate";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (await db.QueryAsync<ListEntry>(sql, new { startDate, endDate })).ToList();
        }


        public async Task<ResultMatch?>GetLastResult()
        {
            string sql = "SELECT TOP (1) FirstName, LastName, Brutto, Netto, " +
                "DamstahlPoints, Points, Hallington, Tee, MatchFormId, OverallWinner, " +
                "MatchDate, MatchResultId, MatchId, HcpIndex, Hcp, Dining, " +
                "Puts, Birdies, [Rank], Official, VgcNo, ClubName, CourseName " +
                "from [ms].[vMatchResult]	" +
                "WHERE ([OverallWinner] = 1) " +
                "ORDER BY MatchDate DESC";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (await db.QueryAsync<ResultMatch>(sql)).FirstOrDefault();
        }


        public async Task<IEnumerable<MatchResult>>GetMatchResults(int matchId)
        {
            using IDbConnection db = new SqlConnection(ConnectionString);

            return await db.QueryAsync<MatchResult>("[ms].[MatchResultSelectWinners] @MatchId", new { matchId });
        }
        public async Task<IEnumerable<MatchResult>?>GetMatchResultForRegistration(int matchId)
        {
            try
            {
                using IDbConnection db = new SqlConnection(ConnectionString);
                return await db.QueryAsync<MatchResult>("[ms].[MatchResultListForRegistration] @MatchId", new { matchId });
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }        
        }

        public async Task<MatchResult> MatchResultUpsert(MatchResult model)
        {
            using var con = new SqlConnection(ConnectionString);

            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ms].[MatchResultUpsert]";
            cmd.Parameters.Add("MatchResultId", SqlDbType.Int).Direction = ParameterDirection.Output;

            cmd.Parameters.AddWithValue("VgcNo", model.VgcNo);
            cmd.Parameters.AddWithValue("MatchId", model.MatchId);
            cmd.Parameters.AddWithValue("Hcp", model.Hcp);
            cmd.Parameters.AddWithValue("HcpGroup", model.HcpGroup);
            cmd.Parameters.AddWithValue("Puts", model.Puts);
            cmd.Parameters.AddWithValue("Brutto", model.Brutto);
            cmd.Parameters.AddWithValue("Points", model.Points);
            cmd.Parameters.AddWithValue("Hallington", model.Hallington);
            cmd.Parameters.AddWithValue("Birdies", model.Birdies);
            cmd.Parameters.AddWithValue("ShootOut", model.ShootOut);
            cmd.Parameters.AddWithValue("Dining", model.Dining);
            cmd.Parameters.AddWithValue("InNearestPin", model.InNearestPin);
            cmd.Parameters.AddWithValue("InBirdies", model.InBirdies);

            cmd.CommandTimeout = 240;
            con.Open();
            var res = await cmd.ExecuteNonQueryAsync();

            object obj = cmd.Parameters["MatchResultId"].Value;
            if (!(obj is DBNull))
                model.MatchResultId = Convert.ToInt32(obj);

            return model;
        }

        public async Task<int> MatchRegistrationUpsert(MatchRegistration model)
        {
            using var con = new SqlConnection(ConnectionString);

            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ms].[MatchRegistrationUpsert]";

            cmd.Parameters.AddWithValue("@VgcNo", model.VgcNo);
            cmd.Parameters.AddWithValue("@MatchId", model.MatchId);
            cmd.Parameters.AddWithValue("@Birdies", model.Birdies);
            cmd.Parameters.AddWithValue("@NearestPin", model.NearestPin);
            cmd.Parameters.AddWithValue("@Dining", model.Dining);

            cmd.CommandTimeout = 240;
            con.Open();
            int i = await cmd.ExecuteNonQueryAsync();
            return Math.Abs(i);
        }

        public async Task<int> MatchResultDelete(int id)
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            var sql = "delete [ms].[MatchResult] where MatchResultId = @id";
            int i = await db.ExecuteAsync(sql, new { Id = id });
            return i;
        }

        public async Task<IEnumerable<MatchBirdieResult>?> GetMatchBirdies(int matchId)
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            string sql = @"select r.Birdies, m.Firstname, m.Lastname
	                        FROM   ms.MatchResult r INNER JOIN
		                            ms.MemberShip  as s ON r.MemberShipId = s.MemberShipId INNER JOIN
		                            ms.Player m ON s.VgcNo = m.VgcNo
	                        WHERE  (r.Birdies > 0) AND (r.MatchId = @MatchId)
	                        ORDER BY m.Lastname";

            var res  = await db.QueryAsync<MatchBirdieResult>(sql, new { matchId });
            return res;
        }
       
        public async Task<int> MatchResultSettlement(int matchId)
        { 
            string sql = "select MatchFormId from ms.Match where MatchId = @matchId";
            using IDbConnection db = new SqlConnection(ConnectionString);
            MatchModel m = (await db.QueryAsync<MatchModel>(sql, new { matchId })).FirstOrDefault();

            if (m.MatchformId == 1)
                sql = "[ms].[MatchResultSettleByStroke]";
            else if (m.MatchformId == 3)
                sql = "[ms].[MatchResultSettleByHallington]";
            else 
                sql = "[ms].[MatchResultSettleByPoints]";

            var result = db.Execute($"{sql} @MatchId", new { MatchId = matchId });
            result = db.Execute($"[ms].[MatchResultSetDamstahlPoints] @MatchId", new { MatchId = matchId });
            return 1;
        }
        

        public int MatchResultSetDamstahlPoints(int matchId)
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            var result = db.Execute(";exec [ms].[MatchResultSetDamstahlPoints] @MatchId",
                new { MatchId = matchId });
            return 0;
        }
        #endregion

        #region Competition
        public async Task<IEnumerable<ListEntry>?> GetCompetitions()
        {
            string sql = @"SELECT [CompetitionId] as [Key] 
                ,[CompetitionText] as [Value] 
                FROM[ms].[Competition] 
                where Active = 1 
                order by listorder";

            try
            {
                using IDbConnection db = new SqlConnection(ConnectionString);
                return await db.QueryAsync<ListEntry>(sql);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<IEnumerable<CompetitionResult>> GetCompetitionResults(int matchId)
        {
            try
            {
                using IDbConnection db = new SqlConnection(ConnectionString);
                return await db.QueryAsync<CompetitionResult>("[ms].[CompetitionResults] @MatchId", new { matchId });
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        public async Task<int> DeleteCompetitionResult(int resultId)
        {
            try
            {
                using IDbConnection db = new SqlConnection(ConnectionString);
                var res = await db.ExecuteAsync("delete [ms].[CompetitionResult] where CompetitionResultId = @resultId", new { resultId });
                return (int)res;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        public async Task<int> UpsertCompetitionResult(CompetitionUpsert dto)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                using var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[ms].[CompetitionResultUpsert]";

                cmd.Parameters.AddWithValue("@MembershipId", dto.MembershipId);
                cmd.Parameters.AddWithValue("@MatchId", dto.MatchId);
                cmd.Parameters.AddWithValue("@CompetitionId", dto.CompetitionId);

                cmd.CommandTimeout = 240;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                return 1;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        //public IEnumerable<Dto.CompetitionResult> GetCompetitionResults(int matchId)
        //{
        //    MSDatabase.EnableAutoSelect = false;
        //    var list = MSDatabase.Query<Dto.CompetitionResult>(";exec [ms].[CompetitionResults] @MatchId"
        //            , new { MatchId = matchId });
        //    return list;
        //}

        //public Dto.CompetitionResult GetCompetitionResult(int matchId, int competitionId)
        //{
        //    MSDatabase.EnableAutoSelect = false;
        //    var dto = MSDatabase.Query<Dto.CompetitionResult>("SELECT [MatchDate],[CompetitionText] " +
        //        ",[CompetitionResultId],[MembershipId],[CompetitionId] " +
        //        ",[VgcNo],[Firstname],[Lastname],[MatchId] " +
        //        " FROM [ms].[vCompetitionResult]" +
        //        " WHERE MatchId = @MatchId  and CompetitionId = @CompetitionId " +
        //        " order by listorder"
        //            , new
        //            {
        //                MatchId = matchId,
        //                CompetitionId = competitionId
        //            }).FirstOrDefault();

        //    return dto;
        //}
        //public Dto.CompetitionResult GetCompetitionResultById(int id)
        //{
        //    MSDatabase.EnableAutoSelect = false;
        //    var dto = MSDatabase.Query<Dto.CompetitionResult>("SELECT [MatchDate],[CompetitionText] " +
        //        ",[CompetitionResultId],[MembershipId],[CompetitionId] " +
        //        ",[VgcNo],[Firstname],[Lastname],[MatchId] " +
        //        " FROM [ms].[vCompetitionResult]" +
        //        " WHERE CompetitionResultId = @CompetitionResultId "
        //            , new
        //            {
        //                CompetitionResultId = id
        //            }).FirstOrDefault();

        //    return dto;
        //}
        #endregion

        #region Match
        private const string matchSelect = @"select  
                [MatchId],[MatchDate],[MatchForm],[MatchText],[ClubId],[Sponsor],[SponsorLogoId],[CourseName]
                ,[Par],RTrim([Tee]) as Tee,[CourseRating],[Slope],[Remarks],[Official],[ClubName],[MatchformId]
                ,[CourseDetailId],[Shootout] 
                 from [ms].[vMatch] ";

        private const string orderBy = " order by MatchDate";

        public async Task<MatchModel?>GetMatch(int id)
        {
            string sql = matchSelect + " where MatchId = @id " + orderBy;

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (MatchModel?)(await db.QueryAsync<MatchModel>(sql, new { id })).FirstOrDefault();
        }
 
        public async Task<IEnumerable<MatchModel>>GetMatchList()
        {
            string sql = matchSelect + orderBy;

            using (IDbConnection db = new SqlConnection(ConnectionString))
            return (IEnumerable<MatchModel>)(await db.QueryAsync<MatchModel>(sql));
        }
        public async Task<IEnumerable<MatchModel>>GetSeasonMatchList(int season)
        {
            string sql = matchSelect + " where Season = @season " + orderBy;

            using (IDbConnection db = new SqlConnection(ConnectionString))
            return (IEnumerable<MatchModel>)(await db.QueryAsync<MatchModel>(sql, new { season }));
        }

        public async Task<MatchModel> MatchUpsert(MatchModel model)       
        {
            using var con = new SqlConnection(ConnectionString);

            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ms].[MatchUpsert]";
            cmd.Parameters.AddWithValue("MatchId", model.MatchId).Direction = ParameterDirection.InputOutput;
            cmd.Parameters.AddWithValue("MatchDate", model.MatchDate);
            cmd.Parameters.AddWithValue("MatchformId", model.MatchformId);
            cmd.Parameters.AddWithValue("CourseDetailId", model.CourseDetailId);
            cmd.Parameters.AddWithValue("Par", model.Par);
            cmd.Parameters.AddWithValue("Description", model.MatchText);
            cmd.Parameters.AddWithValue("Sponsor", model.Sponsor);
            cmd.Parameters.AddWithValue("SponsorLogoId", model.SponsorLogoId);
            cmd.Parameters.AddWithValue("Remarks", model.Remarks);
            cmd.Parameters.AddWithValue("Official", model.Official);
            cmd.Parameters.AddWithValue("Shootout", model.Shootout);
            //cmd.Parameters.AddWithValue("timestamp", model.timestamp);
               
            cmd.CommandTimeout = 240;
            con.Open();
            await cmd.ExecuteNonQueryAsync();

            model.MatchId = (int)cmd.Parameters["MatchId"].Value; 
            return model;
        }
        #endregion

        #region Matchform
        public async Task<IEnumerable<ListEntry>> GetMatchforms()
        {
            string sql = "SELECT [MatchformId] as [Key],[MatchForm] as [Value] FROM [ms].[Matchform]";

            using (IDbConnection db = new SqlConnection(ConnectionString))
                return (await db.QueryAsync<ListEntry>(sql));
        }

        #endregion
    }
}