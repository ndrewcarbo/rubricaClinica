using Microsoft.AspNetCore.Mvc;
using rubricaClinica.Models;
using rubricaClinica.Services;

namespace rubricaClinica.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly AdminService _service;

        public AuthController(AdminService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Verifica(AdminDTO adminDto)
        {
            if (string.IsNullOrWhiteSpace(adminDto.User) || string.IsNullOrWhiteSpace(adminDto.Passw))
                return BadRequest("Credenziali mancanti");

            if (_service.VerificaUsernPass(adminDto))
            {
                return Ok("Login riuscito");
            }

            return Unauthorized();
        }
    }
}
