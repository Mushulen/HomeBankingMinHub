using HomeBankingMinHub.Models;

namespace HomeBankingMinHub.Repositories.Interface
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccounts();
        IEnumerable<Account> GetAccountsByClient(long clientId);
        Account FindById(long id);
        Account FindByNumber(string number);
        void Save(Account account);
    }
}
