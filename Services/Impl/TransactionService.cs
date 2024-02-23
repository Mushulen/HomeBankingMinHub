using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Repositories.Interface;
using HomeBankingMinHub.Utils;

namespace HomeBankingMinHub.Services.Impl
{
    public class TransactionService (ITransactionsRepository transactionsRepository, IAccountRepository accountRepository) : ITransactionService
    {
        public void createNewTransaction(NewTransactionsDTO newtransactionDTO)
        {
            var fromAccount = accountRepository.FindByNumber(newtransactionDTO.FromAccountNumber);
            var toAccount = accountRepository.FindByNumber(newtransactionDTO.ToAccountNumber);

            //Creacion de las transacciones para ambas cuentas.
            transactionsRepository.Save(TransactionVerify.NewTrasactionFromAccount(newtransactionDTO, accountRepository, newtransactionDTO.ToAccountNumber));
            transactionsRepository.Save(TransactionVerify.NewTransactionToAccount(newtransactionDTO, accountRepository, newtransactionDTO.FromAccountNumber));

            //Actualizacion del balance de las cuentas utilizadas
            accountRepository.Save(TransactionVerify.BalanceUpdate(fromAccount, newtransactionDTO.Amount * -1));
            accountRepository.Save(TransactionVerify.BalanceUpdate(toAccount, newtransactionDTO.Amount));
        }
    }
}