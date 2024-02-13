using HomeBankingMinHub.Models;
using HomeBankingMinHub.Repositories;
using HomeBankingMinHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HomeBankingMinHub.Repositories
{
    public class TransactionsRepository : RepositoryBase<Transactions>, ITransactionsRepository
    {
        public TransactionsRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }
        public Transactions FindById(long id)
        {
            return FindByCondition(transaction => transaction.Id == id)
                   .FirstOrDefault();
        }
        public IEnumerable<Transactions> GetAllTransactions()
        {
            return FindAll()
                   .ToList();
        }
        public void Save(Transactions transaction)
        {
            Create(transaction);
            SaveChanges();
        }
    }
}
