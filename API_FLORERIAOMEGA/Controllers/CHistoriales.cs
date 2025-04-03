using API_FLORERIAOMEGA.Models;
using API_FLORERIAOMEGA.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_FLORERIAOMEGA.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CHistoriales : ControllerBase
    {
        private readonly RHistoriales _repo;



        public CHistoriales(RHistoriales histo)
        {
            _repo = histo;
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] MHistoriales Historial)
        {

            if (Historial == null)
                return BadRequest("Producto no válido.");

            // Obtener el ID recién insertado
            var id = await _repo.InsertarHistorial(Historial);

            // Asignar el ID al producto
            Historial.idHistorial = id;

            if (id > 0) // Si el ID fue creado correctamente, responde con el recurso
                return StatusCode(200, "Se creo correctamente el registro de Historial. IdHistorial "+Historial.idHistorial);

            return StatusCode(500, "Algo salió mal al crear el producto.");
        }


    }
}
