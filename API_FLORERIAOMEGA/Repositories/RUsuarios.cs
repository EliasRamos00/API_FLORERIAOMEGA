using API_FLORERIAOMEGA.Models;
using API_FLORERIAOMEGA.Services;
using Dapper;

namespace API_FLORERIAOMEGA.Repositories
{
    public class RUsuarios
    {
        private readonly DatabaseService _dbService;

        public RUsuarios(DatabaseService dbService)
        {
            _dbService = dbService;
        }
        public async Task<IEnumerable<MUsuarios>> ObtenerUsuarios()
        {
            using var conexion = _dbService.CrearConexion();
            return await conexion.QueryAsync<MUsuarios>("SELECT * FROM Usuarios;");
        }

    }
}
