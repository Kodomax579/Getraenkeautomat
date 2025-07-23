namespace GetraenkeautomatVorrat.Models
{
    public class Vorrat
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Preis { get; set; }
        public double Groesse { get; set; }
        public int Anzahl { get; set; }
        public byte[]? Picture { get; set; }

        //public DateOnly MHD { get; set; }
    }
}
