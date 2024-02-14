using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomeBankingMinHub.Utils.DtoLoad
{
    public class AccountDtoLoader
    {
        public static List<AccountDTO> CurrentClientAccounts(IEnumerable<Account> accounts)
        {
            var accountsDTO = new List<AccountDTO>();
            foreach (Account account in accounts)
            {
                var newAccountsDTO = new AccountDTO
                {
                    Id = account.Id,
                    Number = account.Number,
                    CreationDate = account.CreationDate,
                    Balance = account.Balance,
                    Transactions = account.Transactions.Select(tr => new TransactionsDTO
                    {
                        Id = tr.Id,
                        Type = tr.Type.ToString(),
                        Amount = tr.Amount,
                        Description = tr.Description,
                        Date = tr.Date,
                    }).ToList()

                };
                accountsDTO.Add(newAccountsDTO);
            }
            return accountsDTO;
        }
    }
}
