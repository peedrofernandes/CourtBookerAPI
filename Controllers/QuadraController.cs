﻿using CourtBooker.Model;
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
        public async Task<ActionResult<List<Quadra>>> ListarQuadras()
        {
            return await Task.Run(ActionResult<List<Quadra>> () =>
            {
                List<Quadra> result = _service.ListarQuadras();
                return Ok(result);
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Quadra>> BuscarQuadra(int id)
        {
            return await Task.Run(ActionResult<Quadra> () =>
            {
                Quadra? result = _service.BuscarQuadra(id);
                return Ok(result);
            });
        }

        [HttpPost]
        public async Task<ActionResult<Quadra>> AdicionarQuadra([FromBody] Quadra quadra)
        {
            return await Task.Run(ActionResult<Quadra> () =>
            {
                Quadra result = _service.AdicionarQuadra(quadra);
                return CreatedAtAction(nameof(AdicionarQuadra), result);
            });
        }

        [HttpPost("AdicionarQuadraEsporte")]
        public async Task<ActionResult<Quadra>> AdicionarQuadraEsporte(int idQuadra, int idEsporte)
        {
            return await Task.Run(ActionResult<Quadra> () =>
            {
                bool result = _service.AdicionarQuadraEsporte(idQuadra, idEsporte);
                return Ok(result);
            });
        }

        [HttpDelete("ExcluirQuadraEsporte")]
        public async Task<ActionResult<Quadra>> ExcluirQuadraEsporte(int idQuadra, int idEsporte)
        {
            return await Task.Run(ActionResult<Quadra> () =>
            {
                bool result = _service.ExcluirQuadraEsporte(idQuadra, idEsporte);
                return Ok(result);
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirQuadra(int id)
        {
            return await Task.Run(IActionResult () =>
            {
                bool result = _service.ExcluirQuadra(id);
                return Ok(result);
            });
        }
    }
}
