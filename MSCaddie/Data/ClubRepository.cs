using AutoMapper;
using Dapper;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;


namespace MSCaddie.Data
{
    public class ClubRepository : RepositoryBase, IClubRepository
    {
        public ClubRepository(IConfiguration config, ILogger<PlayerRepository> logger, IMapper mapper) : base(config, logger, mapper)
        {
            ;
        }

        #region Club
        public async Task<Club?> GetClub(int id)
        {
            string sql = @"SELECT ClubId, ClubName "
                + "from ms.Club where ClubId = @id";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (Club?)(await db.QueryAsync<Club>(sql, new { id })).FirstOrDefault();
        }

        public async Task<IEnumerable<Club>> GetClubs()
        {
            string sql = @"SELECT ClubId, ClubName FROM "
                + "ms.Club ORDER BY ClubName";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (IEnumerable<Club>)(await db.QueryAsync<Club>(sql));
        }
        public async Task<Club> ClubUpsert(Club model)
        {
            using var con = new SqlConnection(ConnectionString);

            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ms].[ClubUpsert]";
            cmd.Parameters.AddWithValue("ClubId", model.ClubId).Direction = ParameterDirection.InputOutput;
            cmd.Parameters.AddWithValue("ClubName", model.ClubName);

            cmd.CommandTimeout = 240;
            con.Open();
            await cmd.ExecuteNonQueryAsync();

            model.ClubId = (int)cmd.Parameters["ClubId"].Value;
            return model;
        }

        #endregion

        #region Course

        public async Task<CourseInfo?> GetCourse(int id)
        {
            string sql = @"SELECT [CourseName]" +
                    ",[ClubId],[ClubName],[CourseId],[Slope],[CourseRating],[Par],[Tee]" +
                    ",[CourseTeeId],[CourseDetailId],[IsMale] " +
                    "FROM [ms].[vCourseInfo] " +
                    "where CourseDetailId = @id";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (await db.QueryAsync<CourseInfo>(sql, new { id })).FirstOrDefault();
        }


        public async Task<IEnumerable<CourseInfo>> GetCourses(int? clubId, int? courseId)
        {
            using var con = new SqlConnection(ConnectionString);

            string sql = @"SELECT [CourseName],[ClubId],[ClubName],[CourseId],
                            [Slope],[CourseRating],[Par],[Tee],
                            [CourseTeeId],[CourseDetailId],[IsMale]
                        FROM[ms].[vCourseInfo]
                        where IsMale = 1 
                        and(@clubId is null or @clubId = clubId)
                        and(@courseId is null or @courseId = courseId)
                        order by[CourseName], [Tee]";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return await db.QueryAsync<CourseInfo>(sql, new { clubId, courseId });
        }
        public async Task<CourseInfo> CourseUpsert(CourseInfo model)
        {
            using var con = new SqlConnection(ConnectionString);

            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ds].[CourseDetailUpsert]";
            cmd.Parameters.AddWithValue("CourseDetailId", model.CourseDetailId).Direction = ParameterDirection.InputOutput;
            cmd.Parameters.AddWithValue("CourseId", model.CourseId);
            cmd.Parameters.AddWithValue("CourseTeeId", model.CourseTeeId);
            cmd.Parameters.AddWithValue("Par", model.Par);
            cmd.Parameters.AddWithValue("CourseRating", model.CourseRating);
            cmd.Parameters.AddWithValue("Slope", model.Slope);

            cmd.CommandTimeout = 240;
            con.Open();
            await cmd.ExecuteNonQueryAsync();

            model.CourseDetailId = (int)cmd.Parameters["CourseDetailId"].Value;
            return model;
        }
        #endregion

        #region Tee
        public async Task<ListEntry?> GetTee(int teeId)
        {
            string sql = @"SELECT  [CourseTeeId] as [Key]
                    ,RTrim([Tee]) as [Value] 
                    FROM [ms].[CourseTee]
                    where [CourseTeeId] = @teeId";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (ListEntry?)(await db.QueryAsync<ListEntry>(sql, new { teeId }));
        }
        public async Task<IEnumerable<ListEntry>> GetTees()
        {
            string sql = @"SELECT  [CourseTeeId] as [Key]
                    ,RTrim([Tee]) as [Value] 
                    FROM [ms].[CourseTee]";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (IEnumerable<ListEntry>)(await db.QueryAsync<ListEntry>(sql));
        }
        public async Task<ListEntry> TeeUpsert(ListEntry model)
        {
            using var con = new SqlConnection(ConnectionString);

            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ds].[TeeUpsert]";
            cmd.Parameters.AddWithValue("CourseTeeId", model.Key).Direction = ParameterDirection.InputOutput;
            cmd.Parameters.AddWithValue("TeeName", model.Value);

            cmd.CommandTimeout = 240;
            con.Open();
            await cmd.ExecuteNonQueryAsync();

            model.Key = (int)cmd.Parameters["CourseTeeId"].Value;
            return model;
        }
        #endregion
    }
}