using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Repositories.Interface;
using HomeBankingMinHub.Utils.AccAndCardsGen;

namespace HomeBankingMinHub.Services.Impl
{
    public class AccountService(IClientRepository clientRepository, IAccountRepository accountRepository) : IAccountService
    {
        public List<AccountDTO> getAllAccounts()
        {
            var accounts = accountRepository.GetAllAccounts();
            var accountsDTO = new List<AccountDTO>();
            foreach (Account account in accounts)
            {
                AccountDTO accountDTO = new AccountDTO(account);
                accountsDTO.Add(accountDTO);
            }
            return accountsDTO;
        }
        public List<AccountDTO> getAccountsByClientId(long id)
        {
            var accounts = accountRepository.GetAccountsByClient(id);
            var accountsDTO = new List<AccountDTO>();
            foreach (Account account in accounts)
            {
                AccountDTO accountDTO = new AccountDTO(account);
                accountsDTO.Add(accountDTO);
            }
            return accountsDTO;
        }
        public AccountDTO getAccountById(long id)
        {
            AccountDTO accountDTO = new AccountDTO(accountRepository.FindById(id));
            return accountDTO;
        }
        public Account getByNumber(string number)
        {
            return accountRepository.FindByNumber(number);
        }
        public string createNewAccount (string email)
        {
            Client client = clientRepository.FindByEmail(email);
            var accounts = accountRepository.GetAccountsByClient(client.Id);

            if (accounts.Count() >= 3)
            {
                return "No puede tener mas de 3 cuentas.";
            }

            Account newAccount = AccountGeneration.NewAccountGeneration(client);
            accountRepository.Save(newAccount);
            return string.Empty;
        }
    }
}