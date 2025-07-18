using System.Text.Json.Serialization;

namespace rubricaClinica.Models
{
    public class AppuntamentoDTO
    {
        public string Cod { get; set; } = null!;
        public string? Dat_appu { get; set; }  //DateOnly
        public string? Ora_appu { get; set; }  //TimeSpan
        public string? Note { get; set; }
        public string PazCod { get; set; } = null!;

        [JsonIgnore]
        public PazienteDTO? Paz { get; set; }

        //rif?
    }
}
