using Microsoft.AspNetCore.Mvc;
using rubricaClinica.Models;
using rubricaClinica.Services;

namespace rubricaClinica.Controllers
{
    [ApiController]
    [Route("api/appuntamenti")]
    public class AppuntamentoController : Controller
    {
        private readonly AppuntamentoService _service;

        public AppuntamentoController(AppuntamentoService service)
        {
            _service = service;
        }

        [HttpGet("{varCodice}")]
        public ActionResult<AppuntamentoDTO?> VisualizzaAppuntamento(string varCodice)
        {
            if (string.IsNullOrWhiteSpace(varCodice))
                return BadRequest();

            AppuntamentoDTO? ris = _service.CercaPerCodice(varCodice);
            if (ris is not null)
                return Ok(ris);

            return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<AppuntamentoDTO>> ListaClienti()
        {
            return Ok(_service.CercaTutti());
        }

        [HttpPost]
        public IActionResult InserisciAppuntamneto(AppuntamentoDTO appuDto)
        {
            //TODO: Filtri

            if (_service.Inserisci(appuDto))
                return Ok();

            return BadRequest();
        }

        [HttpPut("{varCodice}")]
        public IActionResult AggiornaAppuntamento(string varCodice, AppuntamentoDTO appuDto)
        {
            if (string.IsNullOrWhiteSpace(varCodice))
                return BadRequest();

            appuDto.Cod = varCodice;

            if (_service.Aggiorna(appuDto))
                return Ok();

            return BadRequest();
        }

        [HttpDelete("{varCodice}")]
        public IActionResult EliminaAppuntamento(string varCodice)
        {
            if (string.IsNullOrWhiteSpace(varCodice))
                return BadRequest();

            if (_service.Elimina(varCodice))
                return Ok();

            return BadRequest();
        }
    }
}
