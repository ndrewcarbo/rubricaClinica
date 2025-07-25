using rubricaClinica.Models;
using rubricaClinica.Repos;

namespace rubricaClinica.Services
{
    public class AppuntamentoService : IService<AppuntamentoDTO>
    {
        private readonly AppuntamentoRepositories _repos;
        private readonly PazienteService _pazienteService;

        public AppuntamentoService(AppuntamentoRepositories repo, PazienteService pazienteService)
        {
            _repos = repo;
            _pazienteService = pazienteService;
        }
        public bool Aggiorna(AppuntamentoDTO entity)
        {
            bool risultato = false;

            if (entity.Cod is not null)
            {
                Appuntamento? paz = _repos.CercaPerCodice(entity.Cod);

                if (paz is not null && entity.Dat_appu is not null && entity.Ora_appu is not null)
                {
                    //paz.Data_di_nascita = entity.Dat > DateOnly.FromDateTime(DateTime.Today)  ? entity.Dat : paz.Data_di_nascita;
                    paz.Note = entity.Note is not null ? entity.Note : paz.Note;


                    if (!string.IsNullOrWhiteSpace(entity.Dat_appu) && DateOnly.TryParse(entity.Dat_appu, out var parsedDate))
                    {
                        paz.Data_appu = parsedDate;
                    }
                    if (!string.IsNullOrWhiteSpace(entity.Ora_appu) && TimeOnly.TryParse(entity.Ora_appu, out var parsedOra))
                    {
                        paz.Ora_appu = parsedOra;
                    }

                    risultato = _repos.Update(paz);
                }
                ;
            }

            return risultato;
        }

        public AppuntamentoDTO? CercaPerCodice(string codice)
        {
            AppuntamentoDTO? risultato = null;

            Appuntamento? interv = _repos.CercaPerCodice(codice);
            if (interv is not null)
            {
                risultato = new AppuntamentoDTO()
                {
                    Cod = interv.Codice,
                    Dat_appu = interv.Data_appu?.ToString("yyyy-MM-dd"),
                    Ora_appu = interv.Ora_appu?.ToString("HH-mm"),
                    Note = interv.Note,
                    //Paz = _pazienteService.CercaPerId((int)interv.PazienteRIF)
                };
            }

            return risultato;
        }

        public IEnumerable<AppuntamentoDTO> CercaTutti()
        {
            ICollection<AppuntamentoDTO> risultato = new List<AppuntamentoDTO>();

            IEnumerable<Appuntamento> clienti = _repos.GetAll();
            foreach (Appuntamento paziente in clienti)
            {
                AppuntamentoDTO temp = new AppuntamentoDTO()
                {
                    Cod = paziente.Codice,
                    Dat_appu = paziente?.Data_appu?.ToString("yyyy-MM-dd"),
                    Ora_appu = paziente?.Ora_appu?.ToString("HH-mm"),
                    Note = paziente?.Note
                    //Dat = (DateOnly)paziente.Data_di_nascita,
                };

                risultato.Add(temp);
            }

            return risultato;
        }

        public bool Elimina(string codice)
        {
            bool risultato = false;

            Appuntamento? paz = _repos.CercaPerCodice(codice);
            if (paz is not null)
            {
                risultato = _repos.Delete(paz.AppuntamentoID);
            }

            return risultato;
        }

        public bool Inserisci(AppuntamentoDTO entity)
        {
            Console.WriteLine($"Ricevuto pazCod: {entity.PazCod}");
            if (entity.Dat_appu is null || entity.PazCod is null)
                return false;

            int? cliId = _pazienteService.RestituisciIdPaziente(entity.PazCod);
            Console.WriteLine($"ID restituito: {cliId}");
            if (!cliId.HasValue)
            {
                Console.WriteLine($"Errore: paziente con codice '{entity.PazCod}' non trovato");
                return false;
            }

            //if (entity.PazCod is not null)
            //{
            //    entity.Paz = _pazienteService.CercaPaziente(entity);
            //}

            Appuntamento cli = new Appuntamento()
            {
                Codice = string.IsNullOrWhiteSpace(entity.Cod) ? Guid.NewGuid().ToString() : entity.Cod,
                Note = entity.Note,
                PazienteRIF = (int)cliId,
            };

            if (!string.IsNullOrWhiteSpace(entity.Dat_appu) &&
            DateOnly.TryParseExact(entity.Dat_appu, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var dataApp))
            {
                cli.Data_appu = dataApp;
            }

            if (!string.IsNullOrWhiteSpace(entity.Ora_appu) &&
            TimeOnly.TryParseExact(entity.Ora_appu, "HH:mm", null, System.Globalization.DateTimeStyles.None, out var oraAppu))
            {
                cli.Ora_appu = oraAppu;
            }
                

            return _repos.Create(cli);
        }

        public IEnumerable<AppuntamentoDTO>? ElencoInterventiPerCliente(string codCli)
        {
            List<AppuntamentoDTO> risultato = new List<AppuntamentoDTO>();

            int? idCli = _pazienteService.RestituisciIdPaziente(codCli);
            if (!idCli.HasValue)
                return null;

            IEnumerable<Appuntamento> elenco = _repos.GetByPazienteRif((int)idCli);
            foreach (Appuntamento interv in elenco)
            {

                AppuntamentoDTO temp = new AppuntamentoDTO()
                {
                    Dat_appu = interv.Data_appu?.ToString("yyyy-MM-dd"),
                    Ora_appu = interv.Ora_appu?.ToString("HH:mm"),
                    Note = interv.Note,
                    //Paz = _pazienteService.CercaPerId((int)interv.PazienteRIF)
                    //Cli = _cliService.CercaPerId(interv.ClienteRIF)
                };

                risultato.Add(temp);
            }

            return risultato;

            throw new NotImplementedException();
        }

    }
}
