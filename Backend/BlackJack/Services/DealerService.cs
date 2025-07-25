using BlackJack.DTO;
using BlackJack.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BlackJack.Services
{
    public class DealerService
    {
        List<CardModel> selectedCards;
        public int score;
        public DealerService() 
        {
            score = 0;
            selectedCards = new List<CardModel>();
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

            foreach (var selectedCard in selectedCards)
            {
                score += selectedCard.Number;
            }
            return score;
        }
    }
}
