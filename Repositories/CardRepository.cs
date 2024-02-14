﻿using HomeBankingMinHub.Models;
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
        public Card FindById(long id)
        {
            return FindByCondition(card => card.Id == id)
                   .FirstOrDefault();
        }
        public IEnumerable<Card> GetAllCards()
        {
            return FindAll().
                    ToList();
        }
        public void Save(Card card)
        {
            Create(card);
            SaveChanges();
        }
    }
}
