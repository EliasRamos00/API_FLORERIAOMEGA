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
            //_connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            _connectionString = "Server=34.132.133.29;Database=FloreriaOmega;Uid=admin;Pwd=admin;"; // ******** QUITAR ANTES DE HACER PUSH ********
        }

        public IDbConnection CrearConexion() => new MySqlConnection(_connectionString);

       
    }
}
