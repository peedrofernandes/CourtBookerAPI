using CourtBooker.Model;
using CourtBooker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourtBooker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private AgendamentoService _service = new();

        [HttpGet]
        public async Task<ActionResult<List<Agendamento>>> ListarAgendamentos()
        {
            return await Task.Run(ActionResult<List<Agendamento>> () =>
            {
                List<Agendamento> result = _service.ListarAgendamentos();
                return Ok(result);
            });
        }

        [HttpGet("AgendamentosDoBloco/{idBloco}")]
        public async Task<ActionResult<List<Agendamento>>> ListarAgendamentosBloco(int idBloco)
        {
            return await Task.Run(ActionResult<List<Agendamento>> () =>
            {
                List<Agendamento> result = _service.ListarAgendamentosBloco(idBloco);
                return Ok(result);
            });
        }

        [HttpPost]
        public async Task<ActionResult<Agendamento>> AdicionarAgendamento([FromBody] Agendamento agendamento)
        {
            return await Task.Run(ActionResult<Agendamento> () =>
            {
                _service.AdicionarAgendamento(agendamento);
                return CreatedAtAction(nameof(AdicionarAgendamento), new {agendamento});
            });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirAgendamento(int id)
        {
            return await Task.Run(IActionResult () =>
            {
                bool result = _service.ExcluirAgendamento(id);   
                return Ok(result);
            });
        }
    }
}
