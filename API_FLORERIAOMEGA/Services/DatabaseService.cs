using Dapper;
using Microsoft.AspNetCore.Hosting.Server;
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

        // Método para ejecutar una transacción
        public async Task<T> EjecutarTransaccionAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> operaciones)
        {
            using (var connection = CrearConexion())
            {
                // Abrir la conexión
                 connection.Open();

                // Iniciar una transacción
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Ejecutar las operaciones dentro de la transacción
                        var resultado = await operaciones(connection, transaction);

                        // Commit si todo va bien
                         transaction.Commit();

                        // Retornar el resultado
                        return resultado;
                    }
                    catch (Exception)
                    {
                        // Rollback si ocurre algún error
                         transaction.Rollback();
                        throw; // Re-lanzamos la excepción para manejarla en un nivel superior
                    }
                }
            }
        }


    }
}
