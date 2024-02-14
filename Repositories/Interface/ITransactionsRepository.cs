using HomeBankingMinHub.Models;
using System.Collections.Generic;

namespace HomeBankingMinHub.Repositories.Interface
{
    public interface ITransactionsRepository
    {
        void Save(Transactions transaction);
        Transactions FindById(long id);
        IEnumerable<Transactions> GetAllTransactions();
    }
}
