using BlackJack.DTO;
using BlackJack.Model;

namespace BlackJack.Services
{
    public class BjService
    {
        private List<CardModel> cards;
        private List<CardModel> playerCards;
        private List<CardModel> dealerCards;
        public int Money { get; set; }

        public void InitializeGame(int money)
        {
            Money = money;
            playerCards = new List<CardModel>();
            dealerCards = new List<CardModel>();

            cards = new List<CardModel>();
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] names = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
            Dictionary<string, int> values = new()
            {
                { "2", 2 }, { "3", 3 }, { "4", 4 }, { "5", 5 }, { "6", 6 },
                { "7", 7 }, { "8", 8 }, { "9", 9 }, { "10", 10 },
                { "Jack", 10 }, { "Queen", 10 }, { "King", 10 }, { "Ace", 11 }
            };

            int idCounter = 1;
            foreach (var suit in suits)
            {
                foreach (var name in names)
                {
                    cards.Add(new CardModel
                    {
                        Id = idCounter++,
                        Suit = suit,
                        Name = name,
                        Number = values[name]
                    });
                }
            }
        }

        public CardModelDTO DrawCard(bool isDealer)
        {
            if (cards == null || cards.Count == 0) return null;

            var rnd = new Random();
            var randIndex = rnd.Next(0, cards.Count);
            var card = cards[randIndex];
            cards.RemoveAt(randIndex);

            var targetList = isDealer ? dealerCards : playerCards;
            targetList.Add(card);

            return new CardModelDTO
            {
                Name = card.Name,
                Suit = card.Suit,
                Number = card.Number,
                IsDealer = isDealer,
                TotalScore = GetScore(isDealer),
                IsOverdrawn = GetScore(isDealer) > 21
            };
        }

        public int GetScore(bool isDealer)
        {
            var cards = isDealer ? dealerCards : playerCards;
            return cards.Sum(c => c.Number);
        }

        public WinModelDTO WhoWins()
        {
            int playerScore = GetScore(false);
            int dealerScore = GetScore(true);

            WinModelDTO winModel = new WinModelDTO()
            {
                TotalScorePlayer = playerScore,
                TotalScoreDealer = dealerScore,
                DealerWin = playerScore > 21 || (dealerScore <= 21 && dealerScore >= playerScore),
            };
            if(!winModel.DealerWin)
            {
                winModel.Money = Money * 2;
            }
            return winModel;
        }
    }
}
