using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;

namespace HomeBankingMinHub.Services
{
    public interface IAccountService
    {
        List<AccountDTO> getAllAccounts();
        List<AccountDTO> getAccountsByClientId(long id);
        AccountDTO getAccountById(long id);
        Account getByNumber(string number);
        string createNewAccount(string email);
    }
}