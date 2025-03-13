using API_FLORERIAOMEGA.Models;
using API_FLORERIAOMEGA.Services;
using Dapper;

namespace API_FLORERIAOMEGA.Repositories
{
    public class RCategorias
    {
        private readonly DatabaseService _dbService;

        public RCategorias(DatabaseService dbService)
        {
            _dbService = dbService;
        }
        public async Task<IEnumerable<MCategorias>> ObtenerCategoriasAsync()
        {
            using var conexion = _dbService.CrearConexion();
            return await conexion.QueryAsync<MCategorias>("SELECT * FROM Categorias;");
        }
    }
}
