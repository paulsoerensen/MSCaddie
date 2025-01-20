using AutoMapper;
using Dapper;
using MSCaddie.Shared.Models;
using  MSCaddie.Shared.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;


namespace MSCaddie.Data
{
    public class RepositoryBase
    {
        protected readonly IConfiguration _config;
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;
 

        public RepositoryBase(IConfiguration config, ILogger logger, IMapper mapper)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            SqlConnectionStringBuilder builder = new(ConnectionString);

            builder.ConnectionString = config.GetConnectionString("DefaultConnection");
            if (config["UserId"] != null)
                builder.UserID = config["UserId"];
            if (config["DbPassword"] != null)
                builder.Password = config["DbPassword"];
            ConnectionString = builder.ConnectionString;
        }

        #region Database stuff
        public string ConnectionString { get; private set; }

        private string? _database;
        /// <summary>
        /// Currently only used for information in API
        /// </summary>
        public string Database { 
            get {
                if (_database == null)
                {
                    using var con = new SqlConnection(ConnectionString);
                    _database = con.Database;
                }
                return _database;

            } 
        }
        public async Task<int> ExecuteCommand(string cmdText)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                using var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = cmdText;
                cmd.CommandTimeout = 240;

                con.Open();
                return await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"ExecuteCommand({cmdText}) - {e.Message}");
                return 0;
            }
        }
        #endregion

    }
}