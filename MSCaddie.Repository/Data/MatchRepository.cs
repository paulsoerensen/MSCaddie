using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MSCaddie.Repository.Dtos;
using MSCaddie.Repository.Interfaces;
using MSCaddie.Repository.Models;
using System.Data;
using System.Text.RegularExpressions;


namespace MSCaddie.Repository.Data
{
    public class MatchRepository : RepositoryBase, IMatchRepository
    {
        private int season;
        public MatchRepository(IConfiguration config, 
            ILogger<PlayerRepository> logger,
            IAdminRepository adminRepo,
            IMapper mapper) : base(config, logger, mapper)
        {
            season = adminRepo.Season;
        }

        #region Method: MatchResults

        public async Task<IEnumerable<ListEntryDto>>GetMatchResultDates(DateTime seasonStart)
        {
            return await GetMatchResultDates(seasonStart, seasonStart);
        }

        public  async Task<IEnumerable<ListEntryDto>> GetMatchResultDates(DateTime startDate, DateTime endDate)
        {
            string sql = "exec [ms].[MatchResultSelectDates] @StartDate=startDate, @EndDate= endDate";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (await db.QueryAsync<ListEntryDto>(sql, new { startDate, endDate })).ToList();
        }

        public async Task<MatchResultDto?>GetLastResult()
        {
            string sql = "SELECT TOP (1) FirstName, LastName, Brutto, Netto, " +
                "DamstahlPoints, Points, Hallington, Tee, MatchFormId, OverallWinner, " +
                "MatchDate, MatchResultId, MatchId, HcpIndex, Hcp, Dining, " +
                "Puts, Birdies, [Rank], Official, VgcNo, ClubName, CourseName " +
                "from [ms].[vMatchResult]	" +
                "WHERE ([OverallWinner] = 1) " +
                "ORDER BY MatchDate DESC";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (await db.QueryAsync<MatchResultDto>(sql)).FirstOrDefault();
        }


        public async Task<IEnumerable<MatchResultDto>>GetMatchResults(int matchId)
        {
            using IDbConnection db = new SqlConnection(ConnectionString);

            return await db.QueryAsync<MatchResultDto>("[ms].[MatchResultSelectWinners] @MatchId", new { matchId });
        }
        public async Task<IEnumerable<MatchResultDto>?>GetMatchResultForRegistration(int matchId)
        {
            try
            {
                using IDbConnection db = new SqlConnection(ConnectionString);
                return await db.QueryAsync<MatchResultDto>("[ms].[MatchResultListForRegistration] @MatchId", new { matchId });
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }        
        }

        public async Task<MatchResultDto> MatchResultUpsert(MatchResultDto dto)
        {
            using var con = new SqlConnection(ConnectionString);

            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ms].[MatchResultUpsert]";
            cmd.Parameters.Add("MatchResultId", SqlDbType.Int).Direction = ParameterDirection.Output;

            cmd.Parameters.AddWithValue("VgcNo", dto.VgcNo);
            cmd.Parameters.AddWithValue("MatchId", dto.MatchId);
            cmd.Parameters.AddWithValue("Hcp", dto.Hcp);
            cmd.Parameters.AddWithValue("HcpIndex", dto.HcpIndex);
            cmd.Parameters.AddWithValue("HcpGroup", dto.HcpGroup);
            cmd.Parameters.AddWithValue("Puts", dto.Puts);
            cmd.Parameters.AddWithValue("Brutto", dto.Brutto);
            cmd.Parameters.AddWithValue("Points", dto.Points);
            cmd.Parameters.AddWithValue("Hallington", dto.Hallington);
            cmd.Parameters.AddWithValue("Birdies", dto.Birdies);
            cmd.Parameters.AddWithValue("ShootOut", dto.ShootOut);
            cmd.Parameters.AddWithValue("Dining", dto.Dining);
            cmd.Parameters.AddWithValue("InNearestPin", dto.InNearestPin);
            cmd.Parameters.AddWithValue("InBirdies", dto.InBirdies);

            cmd.CommandTimeout = 240;
            con.Open();
            var res = await cmd.ExecuteNonQueryAsync();

            object obj = cmd.Parameters["MatchResultId"].Value;
            if (!(obj is DBNull))
                dto.MatchResultId = Convert.ToInt32(obj);

            return dto;
        }

        public async Task<int> MatchRegistrationUpsert(MatchRegistrationDto dto)
        {
            using var con = new SqlConnection(ConnectionString);

            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ms].[MatchRegistrationUpsert]";

            cmd.Parameters.AddWithValue("@VgcNo", dto.VgcNo);
            cmd.Parameters.AddWithValue("@MatchId", dto.MatchId);
            cmd.Parameters.AddWithValue("@Birdies", dto.Birdies);
            cmd.Parameters.AddWithValue("@NearestPin", dto.NearestPin);
            cmd.Parameters.AddWithValue("@Dining", dto.Dining);

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

        public async Task<IEnumerable<MatchResultDto>?> GetMatchBirdies(int matchId)
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            string sql = @"select r.Birdies, m.Firstname, m.Lastname
	                        FROM   ms.MatchResult r INNER JOIN
		                            ms.MemberShip  as s ON r.MemberShipId = s.MemberShipId INNER JOIN
		                            ms.Player m ON s.VgcNo = m.VgcNo
	                        WHERE  (r.Birdies > 0) AND (r.MatchId = @MatchId)
	                        ORDER BY r.Birdies, r.HcpIndex, m.Lastname";

            var res  = await db.QueryAsync<MatchResultDto>(sql, new { matchId });
            return res;
        }
       
        public async Task<int> MatchResultSettlement(int matchId)
        { 
            string sql = "select MatchFormId from ms.Match where MatchId = @matchId";
            using IDbConnection db = new SqlConnection(ConnectionString);
            MatchDto m = (await db.QueryAsync<MatchDto>(sql, new { matchId })).FirstOrDefault();

            var result = db.Execute($"[ms].[MatchResultSetHcpGroup] @MatchId", new { MatchId = matchId });
            if (m.MatchformId == 1)
                sql = "[ms].[MatchResultSettleByStroke]";
            else if (m.MatchformId == 3)
                sql = "[ms].[MatchResultSettleByHallington]";
            else 
                sql = "[ms].[MatchResultSettleByPoints]";

            result = db.Execute($"{sql} @MatchId", new { MatchId = matchId });
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
        public async Task<IEnumerable<ListEntryDto>?> GetCompetitions()
        {
            string sql = @"SELECT [CompetitionId] as [KeyId] 
                ,[CompetitionText] as [KeyValue] 
                FROM[ms].[Competition] 
                where Active = 1 
                order by listorder";

            try
            {
                using IDbConnection db = new SqlConnection(ConnectionString);
                return await db.QueryAsync<ListEntryDto>(sql);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<IEnumerable<CompetitionResultDto>> GetCompetitionResults(int matchId)
        {
            try
            {
                string sql = @"	SELECT [CompetitionText],[CompetitionResultId],[CompetitionId] 
		                            ,[VgcNo],[Firstname],[Lastname],[MatchId] 
	                             FROM [ms].[vCompetitionResult] 
	                             WHERE MatchId = @matchId
	                             order by listorder";
                using IDbConnection db = new SqlConnection(ConnectionString);
                return await db.QueryAsync<CompetitionResultDto>(sql, new { matchId });
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
        public async Task<int> UpsertCompetitionResult(CompetitionResultDto dto)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                using var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[ms].[CompetitionResultUpsert]";

                cmd.Parameters.AddWithValue("@CompetitionResultId", dto.CompetitionResultId);
                cmd.Parameters.AddWithValue("@VgcNo", dto.VgcNo);
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
        #endregion

        #region NearestPin

        public async Task<NearestPinResultDto?> GetNearestPinResult(int NearestPinId)
        {
            try
            {
                string sql = @"SELECT [VgcNo],[Firstname],[Lastname],[NearestPinId]
                            ,[MemberShipId],[MatchId],[PinName],[CourseName],[DistanceInCm],[MatchDate] 
                             FROM [ms].[vNearestPin]
                             where NearestPinId = @NearestPinId";

                using IDbConnection db = new SqlConnection(ConnectionString);
                return (NearestPinResultDto?)(await db.QueryAsync<NearestPinResultDto>(sql, new { NearestPinId })).FirstOrDefault();

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<IEnumerable<NearestPinResultDto>?> GetNearestPinResults(int matchId)
        {
            try
            {
                using IDbConnection db = new SqlConnection(ConnectionString);
                return await db.QueryAsync<NearestPinResultDto>("[ms].[MatchResultNearestPin] @MatchId", new { matchId });
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        /*
        create PROCEDURE [ms].[NearestPinUpsert]
	        @VgcNo int,
	        @MatchId int,
	        @PinName varchar(100),
	        @DistanceInCm int,
	        @NearestPinId int OUTPUT
        */
        public async Task<NearestPinResultDto> UpdateNearestPinResult(NearestPinResultDto dto)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                using var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[ms].[NearestPinUpsert]";

                cmd.Parameters.AddWithValue("@VgcNo", dto.VgcNo);
                cmd.Parameters.AddWithValue("@MatchId", dto.MatchId);
                cmd.Parameters.AddWithValue("@PinName", dto.PinName);
                cmd.Parameters.AddWithValue("@DistanceInCm", dto.DistanceInCM);

                var idParam = new SqlParameter("@NearestPinId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.InputOutput,
                    Value = dto.NearestPinId == 0 ? DBNull.Value : dto.NearestPinId
                };
                cmd.Parameters.Add(idParam);

                cmd.CommandTimeout = 240;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                dto.NearestPinId = idParam.Value != DBNull.Value ? Convert.ToInt32(idParam.Value) : dto.NearestPinId;

                return dto;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        public async Task<int> DeleteNearestPinResult(int id)
        {
            try
            {
                using IDbConnection db = new SqlConnection(ConnectionString);
                var res = await db.ExecuteAsync("delete [ms].[NearestPin] where NearestPinId = @id", new { id });
                return (int)res;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion 

        #region Match
        private const string matchSelect = @"select  
                [MatchId],[MatchDate],[MatchForm],[MatchText],[ClubId],[Sponsor],[SponsorLogoId],[CourseName]
                ,[Par],RTrim([Tee]) as Tee,[CourseRating],[Slope],[Remarks],[Official],[ClubName],[MatchformId]
                ,[CourseDetailId],[Shootout] 
                 from [ms].[vMatch] ";

        private const string orderBy = " order by MatchDate";

        public async Task<MatchDto?>GetMatch(int id)
        {
            string sql = matchSelect + " where MatchId = @id " + orderBy;

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (MatchDto?)(await db.QueryAsync<MatchDto>(sql, new { id })).FirstOrDefault();
        }
 
        public async Task<IEnumerable<MatchDto>>GetMatchList()
        {
            string sql = matchSelect + orderBy;

            using (IDbConnection db = new SqlConnection(ConnectionString))
            return (IEnumerable<MatchDto>)(await db.QueryAsync<MatchDto>(sql));
        }
        public async Task<IEnumerable<MatchDto>> GetMatchList(DateTime start, DateTime end)
        {
            string sql = $"{matchSelect} WHERE @Start < MatchDate AND MatchDate < @End {orderBy}";

            using (IDbConnection db = new SqlConnection(ConnectionString))
                return (await db.QueryAsync<MatchDto>(sql, new { Start = start, End = end }));
        }

        public async Task<IEnumerable<MatchDto>>GetSeasonMatchList(int season)
        {
            string sql = matchSelect + " where Season = @season " + orderBy;

            using (IDbConnection db = new SqlConnection(ConnectionString))
            return (IEnumerable<MatchDto>)(await db.QueryAsync<MatchDto>(sql, new { season }));
        }

        public async Task<MatchDto> MatchUpsert(MatchDto dto)       
        {
            using var con = new SqlConnection(ConnectionString);

            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ms].[MatchUpsert]";
            cmd.Parameters.AddWithValue("MatchId", dto.MatchId).Direction = ParameterDirection.InputOutput;
            cmd.Parameters.AddWithValue("MatchDate", dto.MatchDate);
            cmd.Parameters.AddWithValue("MatchformId", dto.MatchformId);
            cmd.Parameters.AddWithValue("CourseDetailId", dto.CourseDetailId);
            cmd.Parameters.AddWithValue("Par", dto.Par);
            cmd.Parameters.AddWithValue("Description", dto.MatchText);
            cmd.Parameters.AddWithValue("Sponsor", dto.Sponsor);
            cmd.Parameters.AddWithValue("SponsorLogoId", dto.SponsorLogoId);
            cmd.Parameters.AddWithValue("Remarks", dto.Remarks);
            cmd.Parameters.AddWithValue("Official", dto.Official);
            cmd.Parameters.AddWithValue("Shootout", dto.Shootout);
            //cmd.Parameters.AddWithValue("timestamp", dto.timestamp);

            cmd.CommandTimeout = 240;
            con.Open();
            await cmd.ExecuteNonQueryAsync();

            dto.MatchId = (int)cmd.Parameters["MatchId"].Value; 
            return dto;
        }
        #endregion

        #region Matchform
        public async Task<IEnumerable<ListEntryDto>> GetMatchforms()
        {
            string sql = "SELECT [MatchformId] as [KeyId],[MatchForm] as [KeyValue] FROM [ms].[Matchform]";

            using (IDbConnection db = new SqlConnection(ConnectionString))
                return (await db.QueryAsync<ListEntryDto>(sql));
        }

        #endregion
    }
}