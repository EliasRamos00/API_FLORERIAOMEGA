using API_FLORERIAOMEGA.Models;
using API_FLORERIAOMEGA.Services;
using Dapper;

namespace API_FLORERIAOMEGA.Repositories
{
    public class RHistoriales
    {

        private readonly DatabaseService _dbService;

        public RHistoriales(DatabaseService dbService)
        {
            _dbService = dbService;
        }
        public async Task<int> InsertarHistorial(MHistoriales historial)
        {
            using var conexion = _dbService.CrearConexion();
            conexion.Open();
            using var transaccion = conexion.BeginTransaction();

            try
            {
                var queryArticulo = @"
            INSERT INTO Historiales (FechaHora, idUsuario, Accion, Clase, Antes, Despues)
            VALUES (@FechaHora, @idUsuario, @Accion, @Clase, @Antes, @Despues);
            SELECT LAST_INSERT_ID();"; // Obtenemos el último ID generado

                // Insertamos el artículo y obtenemos su ID
                var idHistorial = await conexion.ExecuteScalarAsync<int>(queryArticulo, new
                {
                    historial.FechaHora,
                    historial.idUsuario,
                    historial.Accion,
                    historial.Clase,
                    historial.Antes,
                    historial.Despues
                }, transaction: transaccion);

               

                transaccion.Commit();
                return idHistorial; // Retorna el ID del artículo creado
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<MCategorias>> ObtenerTodoHistorial()
        {
            using var conexion = _dbService.CrearConexion();
            return await conexion.QueryAsync<MCategorias>("SELECT * FROM Historiales;");
        }

    }
}
