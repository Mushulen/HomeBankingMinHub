using HomeBankingMinHub.Models.DTO;

namespace HomeBankingMinHub.Services
{
    public interface ICardService
    {
        List<CardDTO> getCardsByClientId(long id);
        void createNewCard(ClientDTO client, NewCardDTO card);
    }
}