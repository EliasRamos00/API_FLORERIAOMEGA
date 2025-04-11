using API_FLORERIAOMEGA.Models;
using API_FLORERIAOMEGA.Repositories;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_FLORERIAOMEGA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CVentas : ControllerBase
    {
        private readonly RVentas _repo;

        public CVentas(RVentas repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetVentas()
        {
            var Ventas = await _repo.ObtenerVentas();
            return Ok(Ventas);
        }

       

        [HttpPost("crear")]
        public async Task<IActionResult> CrearVentaAsync([FromBody] MVentaDTO ventaDTO)
        {
            try
            {
                // Llama al repositorio para crear la venta
                var idVenta = await _repo.CrearVentaAsync(ventaDTO);

                // Retorna el id de la venta creada
                return Ok(new { idVenta });
            }
            catch (Exception ex)
            {
                // Si algo sale mal, retorna un error
                return StatusCode(500, $"Error al crear la venta: {ex.Message}");
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetVenta(int id)
        {
            var producto = await _repo.ObtenerVentaPorID(id);

            if (producto == null)
            {
                return NotFound();  // Retorna 404 si no se encuentra el producto
            }

            return Ok(producto);  // Retorna el producto con un código 200 OK
        }

    }
}
