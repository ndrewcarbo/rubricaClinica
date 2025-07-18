using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rubricaClinica.Models
{
    [Table("appuntamento")]
    public class Appuntamento
    {
        [Key]
        [Column("appuntamentoid")]
        public int AppuntamentoID { get; set; }

        [Column("codice")]
        public string? Codice { get; set; }

        [Column("pazienterif")]
        public int? PazienteRIF { get; set; }

        [Column("dottorerif")]
        public int? DottoreRIF { get; set; }

        [Column("data_appu")]
        public DateOnly? Data_appu { get; set; }

        [Column("ora_appu")]
        public TimeOnly? Ora_appu { get; set; }

        [Column("note")]
        public string? Note { get; set; }

        //[ForeignKey("DottoreRIF")]
        //public virtual Dottore? DottorerifNavigation { get; set; }

        //[ForeignKey("PazienteRIF")]
        //public virtual Paziente? PazienterifNavigation { get; set; }
    }
}
