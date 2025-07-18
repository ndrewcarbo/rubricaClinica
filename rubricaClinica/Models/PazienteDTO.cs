namespace rubricaClinica.Models
{
    public class PazienteDTO
    {
        public string? Cod { get; set; }
        public string Nom { get; set; } = null!;
        public string Cog { get; set; } = null!;
        public string? Ind { get; set; }
        public string? Tel { get; set; }
        public string? Ema { get; set; }
        public string? Dat { get; set; }
    }
}
