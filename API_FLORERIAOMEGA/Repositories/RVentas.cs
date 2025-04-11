using API_FLORERIAOMEGA.Models;
using API_FLORERIAOMEGA.Services;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace API_FLORERIAOMEGA.Repositories
{
    public class RVentas
    {

        private readonly DatabaseService _dbService;

        public RVentas(DatabaseService dbService)
        {
            _dbService = dbService;
        }
        public async Task<IEnumerable<MVentas>> ObtenerVentas()
        {
            using var conexion = _dbService.CrearConexion();
            return await conexion.QueryAsync<MVentas>("SELECT * FROM Ventas;");
        }

        public async Task<int> CrearVentaAsync(MVentaDTO ventaDTO)
        {
            return await _dbService.EjecutarTransaccionAsync<int>(async (connection, transaction) =>
            {
                try
                {
                    // Inserta la venta en la tabla Ventas y obtiene el idVenta generado
                    var queryVenta = "INSERT INTO Ventas (Total, FechaHora, Vendedor, tieneFactura, idSucursal, idCaja) VALUES (@Total, @FechaHora, @Vendedor, @tieneFactura, @idSucursal, @idCaja); SELECT LAST_INSERT_ID();";
                    var idVenta = await connection.ExecuteScalarAsync<int>(queryVenta, new
                    {
                        Total = ventaDTO.Venta.Total,
                        FechaHora = ventaDTO.Venta.FechaHora,
                        Vendedor = ventaDTO.Venta.Vendedor,
                        tieneFactura = ventaDTO.Venta.tieneFactura,
                        idSucursal = ventaDTO.Venta.idSucursal,
                        idCaja = ventaDTO.Venta.idCaja
                    }, transaction);

                    // Inserta los detalles de la venta en la tabla VentasDetalle
                    var queryDetalle = "INSERT INTO VentasDetalle (idArticulo, idVenta, PrecioVenta, Cantidad) VALUES (@idArticulo, @idVenta, @PrecioVenta, @Cantidad)";
                    foreach (var detalle in ventaDTO.Detalle)
                    {
                        await connection.ExecuteAsync(queryDetalle, new
                        {
                            detalle.idArticulo,
                            idVenta,  // Ahora usamos el idVenta obtenido anteriormente
                            detalle.PrecioVenta,
                            detalle.Cantidad
                        }, transaction);
                    }

                    // Retorna el id de la venta
                    return idVenta;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al crear la venta", ex);
                }
            });
        }



        public async Task<MArticulos> ObtenerVentaPorID(int id)
        {
            using var conexion = _dbService.CrearConexion();
            var query = "SELECT * FROM Ventas WHERE idVenta = @Id";
            return await conexion.QueryFirstOrDefaultAsync<MArticulos>(query, new { Id = id });
        }

    }
}
