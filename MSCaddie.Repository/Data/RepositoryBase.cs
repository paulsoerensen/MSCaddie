using AutoMapper;
using MSCaddie.Repository.Dtos;
using MSCaddie.Repository.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;



namespace MSCaddie.Repository.Data
{
    public class RepositoryBase
    {
        protected readonly IConfiguration _config;
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;
        private string servername;
        private string database;


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
            using SqlConnection connection = new SqlConnection(ConnectionString);
            servername = connection.DataSource;
            database = connection.Database;
        }

        #region Database stuff
        public string ConnectionString { get; private set; }
        public string DatabaseServer => servername;
        public string Database => database;

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