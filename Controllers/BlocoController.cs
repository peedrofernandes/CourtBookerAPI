using CourtBooker.Model;
using CourtBooker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourtBooker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlocoController : ControllerBase
    {
        private BlocoService _service = new();

        [HttpGet]
        public async Task<ActionResult<List<Bloco>>> Bloco()
        {
            return await Task.Run(ActionResult<List<Bloco>> () =>
            {
                List<Bloco> result = _service.ListarBlocos();
                return Ok(result);
            });
        }
        [HttpPost]
        public async Task<ActionResult<Bloco>> Bloco([FromBody] Bloco bloco)
        {
            return await Task.Run(ActionResult<Bloco> () =>
            {
                bool result = _service.AdicionarBloco(bloco);
                return Ok(result);
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Bloco(int id)
        {
            return await Task.Run(IActionResult () =>
            {
                bool result = _service.ExcluirBloco(id);
                return Ok(result);
            });
        }
    }
}
