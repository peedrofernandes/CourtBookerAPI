using CourtBooker.Model;
using CourtBooker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourtBooker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EsporteController : ControllerBase
    {
        private EsporteService _service = new();

        [HttpGet]
        public async Task<ActionResult<List<Esporte>>> Esporte()
        {
            return await Task.Run(ActionResult<List<Esporte>> () =>
            {
                List<Esporte> result = _service.ListarEsportes();
                return Ok(result);
            });
        }
        [HttpPost]
        public async Task<ActionResult<Esporte>> Esporte([FromBody] Esporte esporte)
        {
            return await Task.Run(ActionResult<Esporte> () =>
            {
                bool result = _service.AdicionarEsporte(esporte);
                return Ok(result);
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Esporte(int id)
        {
            return await Task.Run(IActionResult () =>
            {
                bool result = _service.ExcluirEsporte(id);
                return Ok(result);
            });
        }
    }
}
