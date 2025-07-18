using rubricaClinica.Models;
using rubricaClinica.Repos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace rubricaClinica.Services
{
    public class PazienteService : IService<PazienteDTO>
    {
        private readonly PazienteRepositories _repo;

        public PazienteService(PazienteRepositories repo)
        {
            _repo = repo; 
        }


        public bool Aggiorna(PazienteDTO entity)
        {
            bool risultato = false;

            if (entity.Cod is not null)
            {
                Paziente? paz = _repo.CercaPerCodice(entity.Cod);

                if (paz is not null && entity.Nom is not null && entity.Cog is not null)
                {
                    paz.Nome = entity.Nom is not null ? entity.Nom : paz.Nome;
                    paz.Cognome = entity.Cog is not null ? entity.Cog : paz.Cognome;
                    paz.Indirizzo = entity.Ind is not null ? entity.Ind : paz.Indirizzo;
                    paz.Telefono = entity.Tel is not null ? entity.Tel : paz.Telefono;
                    //paz.Data_di_nascita = entity.Dat > DateOnly.FromDateTime(DateTime.Today)  ? entity.Dat : paz.Data_di_nascita;
                    paz.Email = entity.Ema is not null ? entity.Ema : paz.Email;

                    if (!string.IsNullOrWhiteSpace(entity.Dat) && DateOnly.TryParse(entity.Dat, out var parsedDate))
                    {
                        paz.Data_di_nascita = parsedDate;
                    }


                    risultato = _repo.Update(paz);
                };
            }

            return risultato;
        }

        public PazienteDTO? CercaPerCodice(string codice)
        {
            PazienteDTO? risultato = null;

            Paziente? paz = _repo.CercaPerCodice(codice);
            if (paz is not null)
            {
                risultato = new PazienteDTO()
                {
                    Cod = paz.Codice,
                    Nom = paz.Nome,
                    Cog = paz.Cognome,
                    Ind = paz.Indirizzo,
                    Dat = paz.Data_di_nascita?.ToString("yyyy-MM-dd"),
                    Tel = paz.Telefono,
                    Ema = paz.Email

                    //appuntamento
                };
            }

            return risultato;
        }

        public PazienteDTO? CercaPerId(int id)
        {
            PazienteDTO? risultato = null;

            Paziente? paz = _repo.GetById(id);
            if (paz is not null)
            {
                risultato = new PazienteDTO()
                {
                    Cod = paz.Codice,
                    Nom = paz.Nome,
                    Cog = paz.Cognome,
                    Ind = paz.Indirizzo,
                    //Dat = (DateOnly)paz.Data_di_nascita,
                    Tel = paz.Telefono,
                    Ema = paz.Email
                };
            }

            return risultato;
        }

        public IEnumerable<PazienteDTO> CercaTutti()
        {
            ICollection<PazienteDTO> risultato = new List<PazienteDTO>();

            IEnumerable<Paziente> clienti = _repo.GetAll();
            foreach (Paziente paziente in clienti)
            {
                PazienteDTO temp = new PazienteDTO()
                {
                    Cod = paziente.Codice,
                    Nom = paziente.Nome,
                    Cog = paziente.Cognome,
                    //Dat = (DateOnly)paziente.Data_di_nascita,
                    Ind = paziente.Indirizzo,
                    Tel = paziente.Telefono,
                    Ema = paziente.Email
                };

                risultato.Add(temp);
            }

            return risultato;
        }

        public bool Elimina(string codice)
        {
            bool risultato = false;

            Paziente? paz = _repo.CercaPerCodice(codice);
            if (paz is not null)
            {
                risultato = _repo.Delete(paz.PazienteID);
            }

            return risultato;
        }

        public bool Inserisci(PazienteDTO entity)
        {
            if (entity.Nom is null || entity.Cog is null)
                return false;

            Paziente cli = new Paziente()
            {
                Codice = string.IsNullOrWhiteSpace(entity.Cod) ? Guid.NewGuid().ToString() : entity.Cod,
                Nome = entity.Nom,
                Cognome = entity.Cog,
                Indirizzo = entity.Ind,
                //Data_di_nascita = entity.Dat,
                Telefono = entity.Tel,
                Email = entity.Ema
            };

            if (!string.IsNullOrWhiteSpace(entity.Dat) &&
            DateOnly.TryParseExact(entity.Dat, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var dataNascita))
            {
                cli.Data_di_nascita = dataNascita;
            }

            return _repo.Create(cli);
        }

        public int? RestituisciIdPaziente(string codice)
        {
            int? risultato = null;

            Paziente? paz = _repo.CercaPerCodice(codice);
            if (paz is not null)
                risultato = paz.PazienteID;

            return risultato;
        }
    }
}
