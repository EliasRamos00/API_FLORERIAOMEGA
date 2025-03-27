using API_FLORERIAOMEGA.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_FLORERIAOMEGA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CUsuarios : ControllerBase
    {
        private readonly RUsuarios _Users;

        public CUsuarios(RUsuarios user)
        {
            _Users = user;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPass()
        {
            var usuario = await _Users.ObtenerUsuarios();
            return Ok(usuario);
        }

    }
}
