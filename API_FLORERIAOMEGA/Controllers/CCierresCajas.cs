using API_FLORERIAOMEGA.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_FLORERIAOMEGA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CCierresCajas : ControllerBase
    {
        private readonly RCierresCajas _repo;

        public CCierresCajas(RCierresCajas repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCierresCajas()
        {
            var cierresCajas = await _repo.ObtenerCierresCajasAsync();
            return Ok(cierresCajas);
        }
    }
}
