using AutoMapper;
using Dapper;
using MSCaddie.Repository.Interfaces;
using MSCaddie.Repository.Dtos;
using MSCaddie.Shared.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MSCaddie.Repository.Data
{
    public class AdminRepository : RepositoryBase, IAdminRepository
    {
        private List<KeyValuePair<int, string>>? settings;

        public AdminRepository(IConfiguration config, ILogger<AdminRepository> logger, IMapper mapper) : base(config, logger, mapper)
        {
            ;
        }

        public string Connectionstring { get => base.ConnectionString; set => throw new NotImplementedException(); }
        public new string DatabaseServer => base.DatabaseServer;
        public new string Database => base.Database;

        public Dictionary<string, string> Info()
        {
            Dictionary<string, string> dict = new()
            {
                { "Database", ConnectionString }
            };
            return dict;
        }

        #region Settings
        public async Task<PropertyDto?> GetSetting(int id)
        {
            var sql = @"SELECT [PropertyId],[DataValue],[SystemType] FROM [ms].[Property] 
                    where PropertyId = PropertyId";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (PropertyDto?)(await db.QueryAsync<PropertyDto>(sql, new { id })).FirstOrDefault();
        }
        public async Task<SettingsDto?> GetSettings()
        {
            string sql = @"SELECT [SettingsId],[Season],[SeasonStart],[SeasonEnd],
                                [MensSectionLogoUrl],[MensSectionShort],[NoOfRoundsRankings],[RyderCupSponsor],
                                [MaxHcpA],[MaxHcpB],[GBAccount],[GBUsername],[GBPassword],[GBGuid]
                            FROM [ms].[Settings]";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            return await db.QueryFirstOrDefaultAsync<SettingsDto>(sql);
        }



        public async Task<int> SettingsUpsert(SettingsDto model)
        {
            try
            { 
                string sql = @"UPDATE [ms].[Settings]
                               SET [Season] = @Season
                                  ,[SeasonStart] = @SeasonStart
                                  ,[SeasonEnd] = @SeasonEnd
                                  ,[MensSectionLogoUrl] = @MensSectionLogoUrl
                                  ,[MensSectionShort] = @MensSectionShort
                                  ,[NoOfRoundsRankings] = @NoOfRoundsRankings
                                  ,[RyderCupSponsor] = @RyderCupSponsor
                                  ,[MaxHcpA] = @MaxHcpA
                                  ,[MaxHcpB] = @MaxHcpB
                                  ,[GBAccount] = @GBAccount
                                  ,[GBUsername] = @GBUsername
                                  ,[GBPassword] = @GBPassword
                                  ,[GBGuid] = @GBGuid 
                             WHERE SettingsId = @SettingsId";


                using (IDbConnection db = new SqlConnection(ConnectionString))
                return await db.ExecuteAsync(sql, new{ model.Season, model.SeasonStart, model.SeasonEnd, model.MensSectionLogoUrl
                        , model.MensSectionShort, model.NoOfRoundsRankings, model.RyderCupSponsor, model.MaxHcpA, model.MaxHcpB
                        , model.GBAccount, model.GBUsername, model.GBPassword, model.GBGuid, model.SettingsId,
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return 0;
            }
        }

        public IEnumerable<PropertyDto> GetSettings(DateTime dt)
        {
            string sql = ";with cte as ( " +
                "select max(validfrom) as dt, PropertyId " +
                "FROM[ms].[Property] " +
                "        where ValidFrom <= @Valid " +
                "        group by PropertyId " +
                ") " +
                "SELECT p.[PropertyId] as Id, [DataValue] , [SystemType], ValidFrom " +
                "FROM [ms].[Property] as p inner join cte on " +
                "p.[PropertyId] = cte.[PropertyId] AND " +
                "p.ValidFrom = cte.dt";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return db.Query<PropertyDto>(sql, new { Valid = dt });
        }

        public List<string> GetPropertyList()
        {
            return Enum.GetNames(typeof(PropertyKey)).ToList();
        }

        public enum PropertyKey
        {
            Season = 0,
            DSeasonStart = 1,
            DSeasonEnd = 2,
            ShowNumberOfEvents = 3,
            ReadMoreEventUrl = 4,
            ReadMoreResultUrl = 5,
            MemberAdminUrl = 6,
            MensSectionLogoUrl = 7,
            MensSection = 8,
            MinRoundsPlayed = 9,
            MensSectionSponsor = 10,
            GroupAUpperBound = 11,
            GroupBUpperBound = 12,
            WsAccount = 13,
            WsUsername = 14,
            WsPassword = 15,
            WsGroupGuid = 16
        }

        public string? WsAccount
        {
            get { return GetPropertyValue<string>(PropertyKey.WsAccount); }
        }

        public string? WsUsername
        {
            get { return GetPropertyValue<string>(PropertyKey.WsUsername); }
        }

        public string? WsPassword
        {
            get { return GetPropertyValue<string>(PropertyKey.WsPassword); }
        }

        public string? WsGroupGuid
        {
            get { return GetPropertyValue<string>(PropertyKey.WsGroupGuid); }
        }

        public DateTime SeasonStart
        {
            get { return new DateTime(GetPropertyValue<int>(PropertyKey.Season), 1, 1); }
        }

        public int Season
        {
            get { return GetPropertyValue<int>(PropertyKey.Season); }
        }

        public DateTime SeasonEnd
        {
            get { return new DateTime(GetPropertyValue<int>(PropertyKey.Season), 12, 31); }
        }
        public TValue? GetPropertyValue<TValue>(string key)
        {
            PropertyKey kx;
            Enum.TryParse(key, true, out kx);
            return GetPropertyValue<TValue>(kx);
        }

        public async Task<int> PropertyValueUpsert<TValue>(string key, TValue value)
        {
            PropertyKey kx;
            Enum.TryParse(key, true, out kx);
            int id = (int)kx;
            string sql = @"update ms.Property set DataValue = @value where PropertyId = @id";

            using IDbConnection db = new SqlConnection(ConnectionString);
            var res = await db.ExecuteScalarAsync(sql, new { value, id  });
            return Convert.ToInt32(res ?? 0);

            //if (property != null)
            //{
            //    // Record exists, perform an update only for the DataValue
            //    property.DataValue = value.ToString();  // Update the DataValue field
            //    //_propertyRepository.UpdateProperty(property);  // Save changes to the repository
            //}
            //else
            //{
            //    throw new InvalidOperationException($"Property with key {key} not found.");
            //}
        }

        protected TValue? GetPropertyValue<TValue>(PropertyKey key)
        {
            if (settings == null)
            {
                settings = GetSettings(DateTime.Now).Select(
                    x => new KeyValuePair<int, string>(x.Id, value: x.DataValue)).ToList();
            }

            var val = settings.Where(x => x.Key == (int)key).FirstOrDefault().Value;
            if (null == val)
                return default;

            switch (key)
            {
                case PropertyKey.MensSectionLogoUrl:
                case PropertyKey.MensSection:
                case PropertyKey.WsAccount:
                case PropertyKey.WsUsername:
                case PropertyKey.WsPassword:
                case PropertyKey.WsGroupGuid:
                    return (TValue)Convert.ChangeType(val, typeof(TValue));
                case PropertyKey.DSeasonStart:
                case PropertyKey.DSeasonEnd:
                    return (TValue)Convert.ChangeType(val, typeof(TValue));
                //case PropertyKey.MensSectionLogoUrl:
                case PropertyKey.Season:
                case PropertyKey.MinRoundsPlayed:
                case PropertyKey.MensSectionSponsor:
                case PropertyKey.GroupAUpperBound:
                case PropertyKey.GroupBUpperBound:
                    return (TValue)Convert.ChangeType(val, typeof(TValue));
            }
            return default(TValue);
        }
        #endregion

        #region User
        string sqlSelect = @"SELECT [PlayerId],[VgcNo],[FirstName] as Name, [Email],[PasswordHash],[PasswordSalt],[VerificationToken]
		                    ,[VerifiedAt],[PasswordResetToken],[ResetTokenExpires],[Role]";

        public async Task<User?> GetUserByEmail(string email)
        {
            string sql = @$"{sqlSelect}
                            FROM [ms].[Player] 
                            WHERE [email]=@email";

            using (IDbConnection db = new SqlConnection(ConnectionString))
                return (User?)(await db.QueryAsync<User?>(sql, new { email })).FirstOrDefault();
        }

        public async Task<User?> GetUserByVgcNo(int vgcNo)
        {
            string sql = @$"{sqlSelect}
                            FROM [ms].[Player] 
                            WHERE [VgcNo]=@vgcNo";

            using (IDbConnection db = new SqlConnection(ConnectionString))
                return (User?)(await db.QueryAsync<User?>(sql, new { vgcNo })).FirstOrDefault();
        }

        public async Task<User?> GetUserByToken(string token)
        {
            string sql = @$"{sqlSelect}
                            FROM [ms].[Player] 
                            WHERE VerificationToken=@token";

            using IDbConnection db = new SqlConnection(ConnectionString);
            return (User?)(await db.QueryAsync<User?>(sql, new { token })).FirstOrDefault();
        }

        public async Task<User?> GetUserByResetToken(string token)
        {
            string sql = @$"{sqlSelect}
                            FROM [ms].[Player] 
                            WHERE PasswordResetToken=@token";

            using (IDbConnection db = new SqlConnection(ConnectionString))
                return (User?)(await db.QueryAsync<User?>(sql, new { token })).FirstOrDefault();
        }

        public async Task<User?> UserUpsert(User model)
        {
            using var con = new SqlConnection(ConnectionString);
            string[] names = model.Name.Split(' ');

            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ms].[PlayerLoginUpsert]";
            cmd.Parameters.AddWithValue("Email", model.Email);
            cmd.Parameters.AddWithValue("VgcNo", model.VgcNo);
            cmd.Parameters.AddWithValue("FirstName", names[0]);
            cmd.Parameters.AddWithValue("LastName", names.GetLength(0) > 1 ? names[0] : "");
            cmd.Parameters.AddWithValue("PasswordHash", model.PasswordHash);
            cmd.Parameters.AddWithValue("PasswordSalt", model.PasswordSalt);
            cmd.Parameters.AddWithValue("VerificationToken", model.VerificationToken);
            cmd.Parameters.AddWithValue("VerifiedAt", model.VerifiedAt);
            cmd.Parameters.AddWithValue("PasswordResetToken", model.PasswordResetToken);
            cmd.Parameters.AddWithValue("ResetTokenExpires", model.ResetTokenExpires);

            cmd.CommandTimeout = 240;
            con.Open();
            await cmd.ExecuteNonQueryAsync();

            //model.CourseDetailId = (int)cmd.Parameters["CourseDetailId"].Value;
            return model;
        }
        #endregion
    }
}