using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rubricaClinica.Models
{
    [Table("amministratore")]
    public class Admin
    {
        [Key]
        [Column("amministratoreid")]
        public int Id { get; set; }

        [Column("username")]
        public string User { get; set; } = null!;

        [Column("passw")]
        public string Pasw { get; set; } = null!;
    }
}
