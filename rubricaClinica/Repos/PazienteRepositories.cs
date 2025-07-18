using rubricaClinica.Context;
using rubricaClinica.Models;

namespace rubricaClinica.Repos
{
    public class PazienteRepositories : IRepo<Paziente>
    {

        private readonly RubricaClinicaContext _context;

        public PazienteRepositories(RubricaClinicaContext context)
        {
            _context = context;
        }


        public bool Create(Paziente entity)
        {
            bool risultato = false;
            try
            {
                _context.Pazienti.Add(entity);
                _context.SaveChanges();
                risultato = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return risultato;
        }

        public bool Delete(int id)
        {
            bool risultato = false;

            try
            {
                Paziente paz = _context.Pazienti.Single(c => c.PazienteID == id);
                _context.Pazienti.Remove(paz);
                _context.SaveChanges();

                risultato = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return risultato;
        }

        public Paziente? GetById(int id)
        {
            return _context.Pazienti.Find(id);
        }

        public IEnumerable<Paziente> GetAll()
        {
            return _context.Pazienti.ToList();
        }

        public bool Update(Paziente entity)
        {
            bool result = false;

            try
            {
                Paziente paz = _context.Pazienti.Single(mod => mod.Codice == entity.Codice);

                entity.PazienteID = paz.PazienteID;
                entity.Codice = entity.Codice is not null ? entity.Codice : paz.Codice;
                entity.Nome = entity.Nome is not null ? entity.Nome : paz.Nome;
                entity.Cognome = entity.Cognome is not null ? entity.Cognome : paz.Cognome;
                entity.Email = entity.Email is not null ? entity.Email : paz.Email;
                entity.Indirizzo = entity.Indirizzo is not null ? entity.Indirizzo : paz.Indirizzo;
                entity.Telefono = entity.Telefono is not null ? entity.Telefono : paz.Telefono;

                _context.Pazienti.Add(entity);
                _context.SaveChanges();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public Paziente? CercaPerCodice(string varCod)
        {
            return _context.Pazienti.FirstOrDefault(c =>  c.Codice == varCod);
        }
    }
}
