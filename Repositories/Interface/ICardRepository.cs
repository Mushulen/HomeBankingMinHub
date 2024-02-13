using HomeBankingMinHub.Models;
using System.Collections.Generic;

namespace HomeBankingMinHub.Repositories.Interface
{
    public interface ICardRepository
    {
        void Save(Card card);
        Card FindById(long id);
        IEnumerable<Card> GetAllCards();
    }
}
