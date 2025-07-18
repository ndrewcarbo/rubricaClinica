using rubricaClinica.Context;
using rubricaClinica.Models;

namespace rubricaClinica.Repos
{
    public class AppuntamentoRepositories : IRepo<Appuntamento>
    {
        private readonly RubricaClinicaContext _context;

        public AppuntamentoRepositories(RubricaClinicaContext context)
        {
            _context = context;
        }

        public bool Create(Appuntamento entity)
        {
            bool risultato = false;

            try
            {
                _context.Appuntamenti.Add(entity);
                _context.SaveChanges();
                risultato = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return risultato;
        }

        public bool Delete(int id)
        {
            bool risultato = false;

            try
            {
                Appuntamento app = _context.Appuntamenti.Single(c => c.AppuntamentoID == id);
                _context.Appuntamenti.Remove(app);
                _context.SaveChanges();

                risultato = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return risultato;
        }

        public IEnumerable<Appuntamento> GetAll()
        {
            return _context.Appuntamenti.ToList();
        }

        public Appuntamento? GetById(int id)
        {
            return _context.Appuntamenti.Find(id);
        }

        public bool Update(Appuntamento entity)
        {
            bool result = false;

            try
            {
                Appuntamento appu = _context.Appuntamenti.Single(mod => mod.Codice == entity.Codice);

                entity.AppuntamentoID = appu.AppuntamentoID;
                entity.Codice = entity.Codice is not null ? entity.Codice : appu.Codice;
                entity.Data_appu = entity.Data_appu is not null ? entity.Data_appu : appu.Data_appu;
                entity.Ora_appu = entity.Ora_appu is not null ? entity.Ora_appu : appu.Ora_appu;
                entity.Note = entity.Note is not null ? entity.Note : appu.Note;

                _context.Appuntamenti.Add(entity);
                _context.SaveChanges();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public Appuntamento? CercaPerCodice(string varCod)
        {
            return _context.Appuntamenti.FirstOrDefault(c => c.Codice == varCod);
        }

        public IEnumerable<Appuntamento> GetByPazienteRif(int rif)
        {
            return _context.Appuntamenti.Where(i => i.PazienteRIF == rif).ToList();
        }
    }
}
