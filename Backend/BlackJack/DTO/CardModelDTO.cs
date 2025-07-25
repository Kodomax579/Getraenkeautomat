namespace BlackJack.DTO
{
    public class CardModelDTO
    {
        public string? Suit { get; set; }
        public string? Name { get; set; }
        public int Number { get; set; }
        public bool IsDealer { get; set; }
        public int TotalScore { get; set; }
        public bool IsOverdrawn { get; set; }
    }
}
