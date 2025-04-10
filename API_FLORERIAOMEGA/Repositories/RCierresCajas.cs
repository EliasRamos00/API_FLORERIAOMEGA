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
            return await conexion.QueryAsync<MCierresCajas>("SELECT * FROM CierresCajas;");
        }

    }
}
