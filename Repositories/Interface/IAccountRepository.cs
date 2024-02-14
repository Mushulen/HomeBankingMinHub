using HomeBankingMinHub.Models;
using System.Collections.Generic;

namespace HomeBankingMinHub.Repositories.Interface
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccounts();
        IEnumerable<Account> GetAccountsByClient(long clientId);
        void Save(Account account);
        Account FindById(long id);
    }
}
