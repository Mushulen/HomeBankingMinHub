using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Models.Enums;
using HomeBankingMinHub.Repositories;
using HomeBankingMinHub.Repositories.Interface;

namespace HomeBankingMinHub.Utils
{
    public class TransactionVerify
    {
        public static string NewTransactionFieldValidation (NewTransactionsDTO newTransaction, IAccountRepository accountRepository)
        {
            string ErrorMessage = string.Empty;

            //Validacion de los campos.
            if (newTransaction.FromAccountNumber == string.Empty || newTransaction.ToAccountNumber == string.Empty)
            {
                ErrorMessage = "Cuenta de origen o cuenta de destino no proporcionada.";
            }

            else if (accountRepository.FindByNumber(newTransaction.ToAccountNumber) == null)
            {
                ErrorMessage = "La cuenta de destino no existe.";
            }

            else if (accountRepository.FindByNumber(newTransaction.FromAccountNumber) == null)
            {
                ErrorMessage = "La cuenta de origen no existe.";
            }

            else if (newTransaction.Description == string.Empty)
            {
                ErrorMessage = "La descripcion no puede estar vacia.";
            }

            else if (newTransaction.FromAccountNumber == newTransaction.ToAccountNumber)
            {
                ErrorMessage = "No se permite la transferencia a la misma cuenta.";
            }

            else if (accountRepository.FindByNumber(newTransaction.FromAccountNumber).Balance < newTransaction.Amount)
            {
                ErrorMessage = "Fondos insuficientes";
            }

            else if (newTransaction.Amount < 500)
            {
                ErrorMessage = "El monto minimo es de $500";
            }

            return ErrorMessage;
        }
        public static Transactions NewTrasactionFromAccount(NewTransactionsDTO newTransactionDTO, IAccountRepository accountsRepository,string toAccount)
        {
            //Creacion de la transaccion a la cuenta origen.
            Account fromAccount = accountsRepository.FindByNumber(newTransactionDTO.FromAccountNumber);
            Transactions newTransaction = (new Transactions
            {
                Type = TransactionType.DEBIT,
                Amount = newTransactionDTO.Amount * -1,
                Description = newTransactionDTO.Description + " " + toAccount,
                AccountId = fromAccount.Id,
                Date = DateTime.Now,
            });

            return newTransaction;
        }
        public static Transactions NewTransactionToAccount(NewTransactionsDTO newTransactionDTO, IAccountRepository accountsRepository, string fromAccount)
        {
            //Creacion de la transaccion a la cuenta destino.
            Account toAccount = accountsRepository.FindByNumber(newTransactionDTO.ToAccountNumber);
            Transactions newTransaction = (new Transactions
            {
                Type = TransactionType.CREDIT,
                Amount = newTransactionDTO.Amount,
                Description = newTransactionDTO.Description + " " + fromAccount,
                AccountId = toAccount.Id,
                Date = DateTime.Now,
            });

            return newTransaction;
        }
        public static Account BalanceUpdate(Account account, double newTransactionAmount)
        {
            account.Balance = account.Balance + newTransactionAmount;
            return account;
        }
    }
}