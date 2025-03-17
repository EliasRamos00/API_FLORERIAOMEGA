using API_FLORERIAOMEGA.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace API_FLORERIAOMEGA.Repositories
{
    public class RArticulos
    {
        private readonly string _connectionString;

        public RArticulos(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public async Task<IEnumerable<MArticulos>> ObtenerProductosAsync()
        {
            using var connection = new MySqlConnection(_connectionString);
            return await connection.QueryAsync<MArticulos>("SELECT * FROM Articulos");
        }

    }
}
