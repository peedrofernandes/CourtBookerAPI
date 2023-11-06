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
        public async Task<ActionResult<List<Esporte>>> ListarEsportes()
        {
            return await Task.Run(ActionResult<List<Esporte>> () =>
            {
                List<Esporte> result = _service.ListarEsportes();
                return Ok(result);
            });
        }
        [HttpPost]
        public async Task<ActionResult<Esporte>> AdicionarEsporte([FromBody] Esporte esporte)
        {
            return await Task.Run(ActionResult<Esporte> () =>
            {
                Esporte result = _service.AdicionarEsporte(esporte);
                return CreatedAtAction(nameof(AdicionarEsporte), esporte);
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirEsporte(int id)
        {
            return await Task.Run(IActionResult () =>
            {
                bool result = _service.ExcluirEsporte(id);
                return Ok(result);
            });
        }
    }
}
