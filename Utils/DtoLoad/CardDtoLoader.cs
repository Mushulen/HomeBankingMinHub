using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Models;

namespace HomeBankingMinHub.Utils.DtoLoad
{
    public class CardDtoLoader
    {
        public static List<CardDTO> CurrentClientCards(IEnumerable<Card> cards)
        {
            var cardsDTO = new List<CardDTO>();
            foreach (Card card in cards)
            {
                var currentCard = new CardDTO
                {
                    Id = card.Id,
                    CardHolder = card.CardHolder,
                    Color = card.Color.ToString(),
                    Type = card.Type.ToString(),
                    Cvv = card.Cvv,
                    FromDate = card.FromDate,
                    Number = card.Number,
                    ThruDate = card.ThruDate,
                };
                cardsDTO.Add(currentCard);
            }
            return cardsDTO;
        }
    }
}
