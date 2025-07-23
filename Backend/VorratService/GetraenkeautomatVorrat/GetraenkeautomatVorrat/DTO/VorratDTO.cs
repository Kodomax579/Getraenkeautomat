namespace GetraenkeautomatVorrat.DTO
{
    public class VorratDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public double Size { get; set; }
        public int Amount { get; set; }
        public byte[]? Picture { get; set; }
    }
}
