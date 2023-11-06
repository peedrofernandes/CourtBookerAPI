using CourtBooker.Model;
using CourtBooker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CourtBooker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        [HttpPost("login")]
        public IActionResult Login(string cpf, string senha)
        {
            Usuario? usuario = new UsuarioService().BuscarUsuario(cpf);

            if (usuario != null)
            {
                if (!senha.Equals(usuario.Senha))
                    return Unauthorized(new { Message = "Senha inválida!" });

                return Ok();
            }

            return Unauthorized(new { Message = "Usuário não cadastrado!" });
        }

        [Authorize]
        [HttpGet("protected")]
        public IActionResult Protected()
        {
            return Ok("Você está autenticado!");
        }
    }

}
