using rubricaClinica.Context;
using rubricaClinica.Models;

namespace rubricaClinica.Repos
{
    public class AdminRepos
    {
        private readonly RubricaClinicaContext _context;

        public AdminRepos(RubricaClinicaContext context)
        {
            _context = context;
        }

        public Admin? GetByCredenziali(string username, string psw)
        {
            return _context.Amministratori.FirstOrDefault(a => a.User == username && a.Pasw == psw);
        }
    }
}
