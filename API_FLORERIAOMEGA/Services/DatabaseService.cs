using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace API_FLORERIAOMEGA.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;



        public DatabaseService(IConfiguration config)
        {
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        }

        public IDbConnection CrearConexion() => new MySqlConnection(_connectionString);

       
    }
}
