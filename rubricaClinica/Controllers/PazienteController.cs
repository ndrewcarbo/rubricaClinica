using Microsoft.AspNetCore.Mvc;
using rubricaClinica.Models;
using rubricaClinica.Services;

namespace rubricaClinica.Controllers
{
    [ApiController]
    [Route("api/pazienti")]
    public class PazienteController : Controller
    {
        private readonly PazienteService _service;

        public PazienteController(PazienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PazienteDTO>> ListaClienti()
        {
            return Ok(_service.CercaTutti());
        }

        [HttpGet("{varCodice}")]
        public ActionResult<PazienteDTO?> VisualizzaCliente(string varCodice)
        {

            if (string.IsNullOrWhiteSpace(varCodice))
                return BadRequest();

            PazienteDTO? risultato = _service.CercaPerCodice(varCodice);
            if (risultato is not null)
                return Ok(risultato);

            return NotFound();
        }

        [HttpPost]
        public IActionResult InserisciCliente(PazienteDTO cliDto)
        {
            if (string.IsNullOrWhiteSpace(cliDto.Nom) || string.IsNullOrWhiteSpace(cliDto.Cog))
                return BadRequest();

            if (_service.Inserisci(cliDto))
                return Ok();

            return BadRequest();
        }

        [HttpDelete("{varCodice}")]
        public IActionResult EliminaCliente(string varCodice)
        {
            if (string.IsNullOrWhiteSpace(varCodice))
                return BadRequest();

            if (_service.Elimina(varCodice))
                return Ok();

            return BadRequest();
        }

        [HttpPut("{varCodice}")]
        public IActionResult AggiornaCliente(string varCodice, PazienteDTO cliDto)
        {
            if (string.IsNullOrWhiteSpace(varCodice) ||
                string.IsNullOrWhiteSpace(cliDto.Nom) ||
                string.IsNullOrWhiteSpace(cliDto.Cog))
                return BadRequest();

            cliDto.Cod = varCodice;

            if (_service.Aggiorna(cliDto))
                return Ok();

            return BadRequest();
        }
    }
}
