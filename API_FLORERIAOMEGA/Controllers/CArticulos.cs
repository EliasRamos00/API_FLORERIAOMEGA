using API_FLORERIAOMEGA.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_FLORERIAOMEGA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CArticulos : ControllerBase
    {

        private readonly RArticulos _repo;

        public CArticulos(RArticulos repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _repo.ObtenerProductosAsync();
            return Ok(productos);
        }
    }
}
