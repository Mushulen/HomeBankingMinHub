using HomeBankingMinHub.Models;
using HomeBankingMinHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HomeBankingMinHub.Repositories
{
    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {
        public CardRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }
        public IEnumerable<Card> GetAllCards()
        {
            return FindAll()
                   .ToList();
        }
        public IEnumerable<Card> GetCardsByClient(long clientId)
        {
            return FindByCondition(cards => cards.ClientId == clientId)
                   .ToList();
        }
        public Card FindById(long id)
        {
            return FindByCondition(card => card.Id == id)
                   .FirstOrDefault();
        }
        public void Save(Card card)
        {
            Create(card);
            SaveChanges();
        }
    }
}
