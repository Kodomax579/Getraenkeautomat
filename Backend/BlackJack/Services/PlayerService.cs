using BlackJack.DTO;
using BlackJack.Model;

namespace BlackJack.Services
{
    public class PlayerService
    {
        List<CardModel> selectedCards;
        public int score;
        public PlayerService()
        {
            selectedCards = new List<CardModel>();
            score = 0;
        }

        public int AddCard(CardModelDTO card)
        {
            CardModel cardModel = new CardModel()
            {
                Name = card.Name,
                Number = card.Number,
                Suit = card.Suit,
            };

            selectedCards.Add(cardModel);

            foreach(var selectedCard in selectedCards)
            {
                score += selectedCard.Number;
            }
            return score;
        }
    }
}
