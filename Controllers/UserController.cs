using CourtBooker.Model;
using CourtBooker.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtBooker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _service = new();

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return await Task.Run(ActionResult<List<User>> () =>
            {
                List<User> result = _service.ListarUsuarios();
                return Ok(result);
            });
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<User>> Get(string cpf)
        {
            return await Task.Run(ActionResult<User> () =>
            {
                User? result = _service.GetUser(cpf);
                return Ok(result);
            });
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
            return await Task.Run(ActionResult<User> () =>
            {
                bool result = _service.AddUser(user);
                return Ok(result);
            });
        }

        [HttpPut]
        public async Task<IActionResult> EditUser([FromBody] User user)
        {
            return await Task.Run(IActionResult () =>
            {
                bool result = _service.PutUser(user);
                return Ok(result);
            });
        }

        [HttpDelete("{cpf}")]
        public async Task<IActionResult> DeleteUser(string cpf)
        {
            return await Task.Run(IActionResult () =>
            {
                bool result = _service.DeleteUser(cpf);
                return Ok(result);
            });
        }
    }
}
