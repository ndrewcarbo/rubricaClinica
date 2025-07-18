using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rubricaClinica.Models
{
    [Table("paziente")]
    public class Paziente
    {
        [Key]
        [Column("pazienteid")]
        public int PazienteID { get; set; }

        [Column("codice")]
        public string? Codice { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = null!;

        [Column("cognome")]
        public string Cognome { get; set; } = null!;

        [Column("indirizzo")]
        public string Indirizzo { get; set; } = null!;

        [Column("telefono")]
        public string Telefono { get; set; } = null!;

        [Column("email")]
        public string Email { get; set; } = null!;

        [Column("data_di_nascita")]
        public DateOnly? Data_di_nascita { get; set; }

        [Column("dottorerif")]
        public int? DottoreRIF { get; set; }

        //[ForeignKey("DottoreRIF")]
        //public virtual Dottore? DottorerifNavigation { get; set; }
    }
}
