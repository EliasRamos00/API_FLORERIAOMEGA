using API_FLORERIAOMEGA.Models;
using API_FLORERIAOMEGA.Services;
using Dapper;
using MySql.Data.MySqlClient;

namespace API_FLORERIAOMEGA.Repositories
{
    public class RArticulos
    {
        private readonly DatabaseService _dbService;

        public RArticulos(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        public async Task<IEnumerable<MArticulos>> ObtenerProductosAsync()
        {
            using var conexion = _dbService.CrearConexion();
            return await conexion.QueryAsync<MArticulos>("SELECT * FROM Articulos");
        }

        public async Task<MArticulos> ObtenerProductoPorIdAsync(int id)
        {
            using var conexion = _dbService.CrearConexion();
            var query = "SELECT * FROM Articulos WHERE idArticulo = @Id";
            return await conexion.QueryFirstOrDefaultAsync<MArticulos>(query, new { Id = id });
        }

        public async Task<int> CrearProductoAsync(MArticulos producto)
        {
            var query = @"
                INSERT INTO Articulos (Foto, Color, Descripcion, Tamanio, CodigoBarras, IdCategoria)
                VALUES (@Foto, @Color, @Descripcion, @Tamanio, @CodigoBarras, @IdCategoria);
                SELECT LAST_INSERT_ID();"; // Obtenemos el último ID generado

            // Usamos ExecuteScalarAsync para obtener el valor del ID
            using (var conexion = _dbService.CrearConexion())
            {
                var id = await conexion.ExecuteScalarAsync<int>(query, new
                {
                    Foto = producto.Foto,
                    Color = producto.Color,
                    Descripcion = producto.Descripcion,
                    Tamanio = producto.Tamanio,
                    CodigoBarras = producto.CodigoBarras,
                    IdCategoria = producto.IdCategoria
                });

                return id; // Devuelve el ID como un entero
            }
        }

        public async Task<bool> UpdateProductoAsync(MArticulos producto)
        {
            using var conexion = _dbService.CrearConexion();
            var query = "UPDATE Articulos SET " +
                        "Foto = @Foto, " +
                        "Color = @Color, " +
                        "Descripcion = @Descripcion, " +
                        "Tamanio = @Tamanio, " +
                        "CodigoBarras = @CodigoBarras, " +
                        "IdCategoria = @IdCategoria " +
                        "WHERE idArticulo = @idArticulo";

            // Ejecutar el UPDATE y verificar cuántas filas fueron afectadas
            var filasAfectadas = await conexion.ExecuteAsync(query, new
            {
                producto.Foto,
                producto.Color,
                producto.Descripcion,
                producto.Tamanio,
                producto.CodigoBarras,
                producto.IdCategoria,
                producto.idArticulo  // Este es el ID del producto que estamos actualizando
            });

            // Si se actualizaron filas (filasAfectadas > 0), significa que el producto se actualizó correctamente
            return filasAfectadas > 0;
        }


        public async Task<bool> DeleteProductoAsync(int id)
        {
            using var conexion = _dbService.CrearConexion();
            var query = "DELETE FROM Articulos WHERE idArticulo = @idArticulo";

            // Ejecutamos el DELETE y verificamos cuántas filas fueron afectadas
            var filasAfectadas = await conexion.ExecuteAsync(query, new { idArticulo = id });

            // Si se eliminaron filas (filasAfectadas > 0), significa que el producto fue encontrado y eliminado
            return filasAfectadas > 0;
        }




    }
}
