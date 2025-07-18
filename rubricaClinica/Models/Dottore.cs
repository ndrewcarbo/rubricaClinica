using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rubricaClinica.Models
{

    [Table("dottore")]
    public class Dottore
    {
        [Key]
        [Column("dottoreID")]
        public int DottoreID { get; set; }
        [Column("usern")]
        public string Usern { get; set; } = null!;
        [Column("passw")]
        public string Passw { get; set; } = null!;
        [Column("email")]
        public string Email { get; set; } = null!;

        //public virtual ICollection<Paziente> Paziente { get; set; } = new List<Paziente>();
    }
}
