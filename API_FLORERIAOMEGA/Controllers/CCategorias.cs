using API_FLORERIAOMEGA.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_FLORERIAOMEGA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CCategorias : ControllerBase
    {

        private readonly RCategorias _repo;

        public CCategorias(RCategorias repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetSucursales()
        {
            var sucursales = await _repo.ObtenerCategoriasAsync();
            return Ok(sucursales);
        }


    }
}
