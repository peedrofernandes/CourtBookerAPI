using CourtBooker.Model;
using CourtBooker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourtBooker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuadraController : ControllerBase
    {
        private QuadraService _service = new();

        [HttpGet]
        public async Task<ActionResult<List<Quadra>>> Quadra()
        {
            return await Task.Run(ActionResult<List<Quadra>> () =>
            {
                List<Quadra> result = _service.ListarQuadras();
                return Ok(result);
            });
        }

        [HttpPost]
        public async Task<ActionResult<Quadra>> Quadra([FromBody] Quadra quadra)
        {
            return await Task.Run(ActionResult<Quadra> () =>
            {
                bool result = _service.AdicionarQuadra(quadra);
                return Ok(result);
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Quadra(int id)
        {
            return await Task.Run(IActionResult () =>
            {
                bool result = _service.ExcluirQuadra(id);
                return Ok(result);
            });
        }
    }
}
