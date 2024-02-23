using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Repositories.Interface;
using HomeBankingMinHub.Utils.AccAndCardsGen;

namespace HomeBankingMinHub.Services.Impl
{
    public class CardService(ICardRepository cardRepository) : ICardService
    {
        public List<CardDTO> getCardsByClientId(long id)
        {
            var cards = cardRepository.GetCardsByClient(id);
            var cardsDTO = new List<CardDTO>();
            foreach (Card card in cards)
            {
                CardDTO cardDTO = new CardDTO(card);
                cardsDTO.Add(cardDTO);
            }
            return cardsDTO;
        }
        public void createNewCard(ClientDTO currentClient, NewCardDTO card)
        {
            Card newGeneratedCard =  CardGeneration.NewCardGeneration(currentClient, card);
            cardRepository.Save(newGeneratedCard);
        }
    }
}