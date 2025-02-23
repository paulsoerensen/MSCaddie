using AutoMapper;
using Dapper;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;
using MSCaddie.Shared.Dtos;


namespace MSCaddie.Data
{
    public class ClubRepository : RepositoryBase, IClubRepository
    {
        public ClubRepository(IConfiguration config, ILogger<PlayerRepository> logger, IMapper mapper) : base(config, logger, mapper)
        {
            ;
        }

        #region Club
        public async Task<ClubDto?> GetClub(int id)
        {
            string sql = @"SELECT ClubId, ClubName "
                + "from ms.Club where ClubId = @id";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (ClubDto?)(await db.QueryAsync<ClubDto>(sql, new { id })).FirstOrDefault();
        }

        public async Task<IEnumerable<ClubDto>> GetClubs()
        {
            string sql = @"SELECT ClubId, ClubName FROM "
                + "ms.Club ORDER BY ClubName";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (IEnumerable<ClubDto>)(await db.QueryAsync<ClubDto>(sql));
        }
        public async Task<ClubDto> ClubUpsert(ClubDto model)
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

        public async Task<CourseDto?> GetCourse(int id)
        {
            string sql = @"SELECT [CourseName]" +
                    ",[ClubId],[ClubName],[CourseId],[Slope],[CourseRating],[Par],[Tee]" +
                    ",[CourseTeeId],[CourseDetailId],[IsMale] " +
                    "FROM [ms].[vCourseInfo] " +
                    "where CourseDetailId = @id";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (await db.QueryAsync<CourseDto>(sql, new { id })).FirstOrDefault();
        }


        public async Task<IEnumerable<CourseDto>> GetCourses(int? clubId, int? courseId)
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
            return await db.QueryAsync<CourseDto>(sql, new { clubId, courseId });
        }
        public async Task<CourseDto> CourseUpsert(CourseDto dto)
        {
            using var con = new SqlConnection(ConnectionString);

            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ds].[CourseDetailUpsert]";
            cmd.Parameters.AddWithValue("CourseDetailId", dto.CourseDetailId).Direction = ParameterDirection.InputOutput;
            cmd.Parameters.AddWithValue("CourseId", dto.CourseId);
            cmd.Parameters.AddWithValue("CourseTeeId", dto.CourseTeeId);
            cmd.Parameters.AddWithValue("Par", dto.Par);
            cmd.Parameters.AddWithValue("CourseRating", dto.CourseRating);
            cmd.Parameters.AddWithValue("Slope", dto.Slope);

            cmd.CommandTimeout = 240;
            con.Open();
            await cmd.ExecuteNonQueryAsync();

            dto.CourseDetailId = (int)cmd.Parameters["CourseDetailId"].Value;
            return dto;
        }
        #endregion

        #region Tee
        public async Task<ListEntryDto?> GetTee(int teeId)
        {
            string sql = @"SELECT  [CourseTeeId] as [KeyId]
                    ,RTrim([Tee]) as [KeyValue] 
                    FROM [ms].[CourseTee]
                    where [CourseTeeId] = @teeId";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (await db.QueryAsync<ListEntryDto>(sql, new { teeId })).FirstOrDefault();
        }
        public async Task<IEnumerable<ListEntryDto>> GetTees()
        {
            string sql = @"SELECT  [CourseTeeId] as [KeyId]
                    ,RTrim([Tee]) as [KeyValue] 
                    FROM [ms].[CourseTee]";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return await db.QueryAsync<ListEntryDto>(sql);
        }
        public async Task<ListEntryDto> TeeUpsert(ListEntryDto dto)
        {
            using var con = new SqlConnection(ConnectionString);

            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ds].[TeeUpsert]";
            cmd.Parameters.AddWithValue("CourseTeeId", dto.KeyId).Direction = ParameterDirection.InputOutput;
            cmd.Parameters.AddWithValue("TeeName", dto.KeyValue);

            cmd.CommandTimeout = 240;
            con.Open();
            await cmd.ExecuteNonQueryAsync();

            dto.KeyId = (int)cmd.Parameters["CourseTeeId"].Value;
            return dto;
        }
        #endregion
    }
}