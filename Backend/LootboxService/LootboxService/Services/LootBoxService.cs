using LootboxService.Models;

namespace LootboxService.Services
{
    public class LootBoxService
    {
        static List<Lootbox> Lootboxes { get; }
        static Random rnd = new Random();
        static LootBoxService()
        {
            Lootboxes = new List<Lootbox>
            {
            new Lootbox { Id = 1, Name = "Common", Price = 1, Probability = 40 },
            new Lootbox { Id = 2, Name = "Uncommon", Price = 5, Probability = 35 },
            new Lootbox { Id = 3, Name = "Rare", Price = 10, Probability = 30 },
            new Lootbox { Id = 4, Name = "Epic", Price = 25, Probability = 30 },
            new Lootbox { Id = 5, Name = "Legendary", Price = 50, Probability = 25 },
            new Lootbox { Id = 6, Name = "Mhytic", Price = 100, Probability = 20 },
            };
        }

        public static List<Lootbox> GetAll() => Lootboxes;

        public static Lootbox? Get(int id) => Lootboxes.FirstOrDefault(p => p.Id == id);

        public static int Start(int lbId)
        {
            var index = Lootboxes.FindIndex(p => p.Id == lbId);
            if (index == -1) return -1;

            int probability = Lootboxes[index].Probability;
            int random = rnd.Next(0, 100);
            if (random <= probability) return Lootboxes[index].Price * 2;
            else return 0;
        }
    }
}
