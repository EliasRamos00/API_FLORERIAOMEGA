using API_FLORERIAOMEGA.Models;
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

        [HttpGet("totalsistema/{idCaja}/{idSucursal}")]
        public async Task<IActionResult> GetProductosInventario(int idCaja, int idSucursal)
        {
            var total = await _repo.ObtenerTotalSistema(idCaja,idSucursal);
            var dTO = new GenericMessageDTO { data = total.ToString("C2") };

            return Ok(dTO);
        }


        [HttpPost]
        public async Task<IActionResult> PostCierreCaja([FromBody] MCierresCajas cierreCajas)
        {
            var result = await _repo.InsertarCierreCaja(cierreCajas);
            return Ok(result);
        }

    }
}
