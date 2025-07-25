using rubricaClinica.Models;
using rubricaClinica.Repos;

namespace rubricaClinica.Services
{
    public class AdminService
    {
        private readonly AdminRepos _repos;

        public AdminService(AdminRepos repos)
        {
            _repos = repos;
        }

        public bool VerificaUsernPass(AdminDTO adminDTO)
        {
            bool risult = false;

            if(adminDTO.User is not null && adminDTO.Passw is not null)
            {
                Admin? admin = _repos.GetByCredenziali(adminDTO.User, adminDTO.Passw);

                if (admin is not null)
                    risult = true;
            }

            return risult;
        }
    }
}
