using HomeBankingMinHub.Models;
using System.Collections.Generic;

namespace HomeBankingMinHub.Repositories.Interface
{
    public interface ITransactionsRepository
    {
        IEnumerable<Transactions> GetAllTransactions();
        Transactions FindById(long id);
        Transactions FindByNumber(long id);
        void Save(Transactions transaction);
    }
}
