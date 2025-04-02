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

        public async Task<IEnumerable<MArticulosDTO>> ObtenerProductosInventarioAsync()
        {
            using var conexion = _dbService.CrearConexion();
            return await conexion.QueryAsync<MArticulosDTO>("SELECT * FROM " +
                "Articulos A " +
                "INNER JOIN Inventarios I " +
                "ON A.idArticulo = I.idArticulo ");
        }


        public async Task<MArticulos> ObtenerProductoPorIdAsync(int id)
        {
            using var conexion = _dbService.CrearConexion();
            var query = "SELECT * FROM Articulos WHERE idArticulo = @Id";
            return await conexion.QueryFirstOrDefaultAsync<MArticulos>(query, new { Id = id });
        }

        public async Task<int> CrearProductoAsync(MArticulosDTO producto)
        {
            using var conexion = _dbService.CrearConexion();
            conexion.Open();
            using var transaccion =  conexion.BeginTransaction();

            try
            {
                var queryArticulo = @"
            INSERT INTO Articulos (Foto, Color, Descripcion, Tamanio, CodigoBarras, IdCategoria)
            VALUES (@Foto, @Color, @Descripcion, @Tamanio, @CodigoBarras, @IdCategoria);
            SELECT LAST_INSERT_ID();"; // Obtenemos el último ID generado

                // Insertamos el artículo y obtenemos su ID
                var idArticulo = await conexion.ExecuteScalarAsync<int>(queryArticulo, new
                {
                    producto.Foto,
                    producto.Color,
                    producto.Descripcion,
                    producto.Tamanio,
                    producto.CodigoBarras,
                    producto.IdCategoria
                }, transaction: transaccion);

                var queryInventario = @"
            INSERT INTO Inventarios (idArticulo, Stock, Min, Max, PrecioVenta, PrecioCompra)
            VALUES (@idArticulo, @Stock, @Min, @Max, @PrecioVenta, @PrecioCompra);";

                // Insertamos el inventario asociado al artículo
                await conexion.ExecuteAsync(queryInventario, new
                {
                    idArticulo,
                    producto.Stock,
                    producto.Min,
                    producto.Max,
                    producto.PrecioVenta,
                    producto.PrecioCompra
                }, transaction: transaccion);

                 transaccion.Commit();
                return idArticulo; // Retorna el ID del artículo creado
            }
            catch
            {
                 transaccion.Rollback();
                throw;
            }
        }


        public async Task<bool> UpdateProductoAsync(MArticulosDTO producto)
        {
            using var conexion = _dbService.CrearConexion();
            conexion.Open();              
            using var transaccion = conexion.BeginTransaction();

            try
            {
                var queryArticulo = @"
            UPDATE Articulos SET 
                Foto = @Foto, 
                Color = @Color, 
                Descripcion = @Descripcion, 
                Tamanio = @Tamanio, 
                CodigoBarras = @CodigoBarras, 
                IdCategoria = @IdCategoria
            WHERE idArticulo = @idArticulo";

                var filasAfectadasArticulos = await conexion.ExecuteAsync(queryArticulo, new
                {
                    producto.Foto,
                    producto.Color,
                    producto.Descripcion,
                    producto.Tamanio,
                    producto.CodigoBarras,
                    producto.IdCategoria,
                    producto.idArticulo
                }, transaction: transaccion);

                var queryInventario = @"
            UPDATE Inventarios SET 
                Stock = @Stock, 
                Min = @Min, 
                Max = @Max, 
                PrecioVenta = @PrecioVenta, 
                PrecioCompra = @PrecioCompra
            WHERE idInventario = @idInventario";

                var filasAfectadasInventario = await conexion.ExecuteAsync(queryInventario, new
                {
                    producto.Stock,
                    producto.Min,
                    producto.Max,
                    producto.PrecioVenta,
                    producto.PrecioCompra,
                    producto.idInventario
                }, transaction: transaccion);

                // Verificamos si ambas actualizaciones afectaron filas
                if (filasAfectadasArticulos > 0 && filasAfectadasInventario > 0)
                {
                     transaccion.Commit();
                    return true;
                }
                else
                {
                     transaccion.Rollback();
                    return false;
                }
            }
            catch
            {
                 transaccion.Rollback();
                throw;
            }
        }



        public async Task<bool> DeleteProductoAsync(int id)
        {
            using var conexion = _dbService.CrearConexion();
            var query = "" +
                "DELETE FROM Inventarios WHERE idArticulo = @idArticulo; " +
                "DELETE FROM Articulos WHERE idArticulo = @idArticulo; ";

            // Ejecutamos el DELETE y verificamos cuántas filas fueron afectadas
            var filasAfectadas = await conexion.ExecuteAsync(query, new { idArticulo = id });

            // Si se eliminaron filas (filasAfectadas > 0), significa que el producto fue encontrado y eliminado
            return filasAfectadas > 0;
        }




    }
}
