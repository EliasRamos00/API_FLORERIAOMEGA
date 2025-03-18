using API_FLORERIAOMEGA.Models;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _repo.ObtenerProductoPorIdAsync(id);

            if (producto == null)
            {
                return NotFound();  // Retorna 404 si no se encuentra el producto
            }

            return Ok(producto);  // Retorna el producto con un código 200 OK
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] MArticulos producto)
        {

            if (producto == null)
                return BadRequest("Producto no válido.");

            // Obtener el ID recién insertado
            var id = await _repo.CrearProductoAsync(producto);

            // Asignar el ID al producto
            producto.idArticulo = id;

            if (id > 0) // Si el ID fue creado correctamente, responde con el recurso
                return CreatedAtAction(nameof(GetProducto), new { id = id }, producto);

            return StatusCode(500, "Algo salió mal al crear el producto.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] MArticulos producto)
        {
            if (producto == null || producto.idArticulo != id)
                return BadRequest("Datos inválidos.");

            // Actualizar el producto
            var resultado = await _repo.UpdateProductoAsync(producto);

            if (resultado)
                return Ok(producto);  // Si la actualización fue exitosa, retornamos el producto actualizado
            else
                return NotFound("Producto no encontrado.");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var resultado = await _repo.DeleteProductoAsync(id);

            if (resultado)
                return NoContent();  // Si el producto se eliminó correctamente, retornamos un estado 204 (sin contenido)
            else
                return NotFound("Producto no encontrado.");  // Si no se encontró el producto, retornamos un estado 404
        }




    }
}
