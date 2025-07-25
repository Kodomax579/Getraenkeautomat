namespace LootboxService.Models
{
    public class Lootbox
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Probability { get; set; }
    }
}
