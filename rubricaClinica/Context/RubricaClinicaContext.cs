using Microsoft.EntityFrameworkCore;
using rubricaClinica.Models;

namespace rubricaClinica.Context
{
    public class RubricaClinicaContext : DbContext
    {
        public RubricaClinicaContext(DbContextOptions<RubricaClinicaContext> options) : base(options) { }

        public DbSet<Paziente> Pazienti { get; set; }
        public DbSet<Dottore> Dottori { get; set; }
        public DbSet<Appuntamento> Appuntamenti { get; set; }
        public DbSet<Admin> Amministratori { get; set; }
    }
}
