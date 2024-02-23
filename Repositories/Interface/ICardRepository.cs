using HomeBankingMinHub.Models;

namespace HomeBankingMinHub.Repositories.Interface
{
    public interface ICardRepository
    {
        IEnumerable<Card> GetAllCards();
        IEnumerable<Card> GetCardsByClient(long clientId);
        Card FindById(long id);
        void Save(Card card);
    }
}
