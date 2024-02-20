using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Models.Enums;
using HomeBankingMinHub.Repositories;
using HomeBankingMinHub.Repositories.Interface;
using Microsoft.Identity.Client;

namespace HomeBankingMinHub.Utils.ClientLoanVrf
{
    public class LoanApplicationVrf (LoanApplicationDTO loanApplicationDTO, Client client, ILoanRepository loanRepository, IAccountRepository accountRepository, IClientRepository clientRepository)
    {
        public string ErrorMessage = string.Empty;
        public Loan loan = loanRepository.FindById(loanApplicationDTO.LoanId);
        public Account account = accountRepository.FindByNumber(loanApplicationDTO.ToAccountNumber);
        public Client verifiedClient = client;
        public string LoanApplicationDataVrf()
        {
            Client toClient = clientRepository.FindById(account.ClientId);
            if (loan == null) { ErrorMessage = "El prestamo no existe"; }
            else if (loanApplicationDTO.Amount <= 0 || loanApplicationDTO.Amount > loan.MaxAmount) { ErrorMessage = "La cantidad requerida es incorrecta"; }
            else if (loanApplicationDTO.Payments == null || !loan.Payments.Contains(loanApplicationDTO.Payments)) { ErrorMessage = "Las cuotas requeridas son invalidas"; }
            else if (verifiedClient.Id != toClient.Id) { ErrorMessage = "La cuenta destino no pertenece a este cliente"; }
            return ErrorMessage;
        }

        public ClientLoan ClientLoanVerifiedGeneration()
        {
            ClientLoan newClientLoan = new ClientLoan
            {
                Amount = loanApplicationDTO.Amount * 1.2,
                Payments = loanApplicationDTO.Payments,
                ClientId = verifiedClient.Id,
                LoanId = loan.Id,
            };

            return newClientLoan;
        }
        public Transactions LoanTransactionGeneration()
        {
            Transactions newTransaction = new Transactions
            {
                Type = TransactionType.CREDIT,
                Amount = loanApplicationDTO.Amount,
                Description = loan.Name + " Loan Approved",
                Date = DateTime.Now,
                AccountId = account.Id
            };

            return newTransaction;
        }
    }
}