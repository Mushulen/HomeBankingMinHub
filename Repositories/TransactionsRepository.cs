using HomeBankingMinHub.Models;
using HomeBankingMinHub.Repositories.Interface;

namespace HomeBankingMinHub.Repositories
{
    public class TransactionsRepository : RepositoryBase<Transactions>, ITransactionsRepository
    {
        public TransactionsRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }
        public IEnumerable<Transactions> GetAllTransactions()
        {
            return FindAll()
            .ToList();
        }
        public Transactions FindById(long id)
        {
            return FindByCondition(transaction => transaction.Id == id)
            .FirstOrDefault();
        }
        public Transactions FindByNumber(long id)
        {
            return FindByCondition(transaction => transaction.Id == id).FirstOrDefault();
        }
        public void Save(Transactions transaction)
        {
            Create(transaction);
            SaveChanges();
        }
    }
}
