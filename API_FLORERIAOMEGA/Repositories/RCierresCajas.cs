using API_FLORERIAOMEGA.Models;
using API_FLORERIAOMEGA.Services;
using Dapper;

namespace API_FLORERIAOMEGA.Repositories
{
    public class RCierresCajas
    {
        private readonly DatabaseService _dbService;

        public RCierresCajas(DatabaseService dbService)
        {
            _dbService = dbService;
        }


        public async Task<IEnumerable<MCierresCajas>> ObtenerCierresCajasAsync()
        {
            using var conexion = _dbService.CrearConexion();
            return await conexion.QueryAsync<MCierresCajas>("SELECT * FROM CierresCajas Order By Fecha DESC;");
        }

        internal async Task<MCierresCajas?> InsertarCierreCaja(MCierresCajas cierreCajas)
        {
            using var conexion = _dbService.CrearConexion();
            var query = ("INSERT INTO CierresCajas (Fecha,Hora,idUsuario,TotalSistema,TotalFisico,idCaja) " +
                "VALUES (@Fecha,@Hora,@idUsuario,@TotalSistema,@TotalFisico,@idCaja); " +
                "SELECT LAST_INSERT_ID();");

            var idCierreCaja = await conexion.ExecuteScalarAsync<int?>(query,
            new
            {
                Fecha = new DateTime(cierreCajas.Fecha.Year, cierreCajas.Fecha.Month, cierreCajas.Fecha.Day),
                cierreCajas.Hora,
                cierreCajas.idUsuario,
                cierreCajas.TotalSistema,
                cierreCajas.TotalFisico,
                cierreCajas.idCaja
            });

            if (idCierreCaja.HasValue)
            {
                cierreCajas.idCierreCaja = idCierreCaja.Value;
                return cierreCajas;
            }

            return null;
        }

        internal async Task<decimal> ObtenerTotalSistema(int idCaja, int idSucursal)
        {
            using var conexion = _dbService.CrearConexion();
            var query = ("SELECT \r\n" +
                "SUM(Total) as TotalSumado,\r\n" +
                "C.idCaja\r\n" +
                "FROM Ventas V\r\n" +
                "INNER JOIN Cajas C ON C.idCaja = V.idCaja\r\n" +
                "WHERE V.idCaja = @idCaja AND C.idSucursal = @idSucursal  \r\n" +
                "AND V.FechaHora BETWEEN @FechaIni AND @FechaFin" +
                "\r\nGROUP BY idCaja");

            var TotalSumado = await conexion.ExecuteScalarAsync<decimal>(query, 
            new
            {
                FechaIni = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,0,0,0),
                FechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59),
                idCaja,
                idSucursal,               
            });
            return TotalSumado;

        }

      
    }
}
