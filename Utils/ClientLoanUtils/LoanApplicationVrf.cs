using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Models.Enums;
using HomeBankingMinHub.Services;

namespace HomeBankingMinHub.Utils.ClientLoanVrf
{
    public class LoanApplicationVrf (LoanApplicationDTO loanApplicationDTO, ClientDTO client, ILoanService loanService, IAccountService accountService, IClientService clientService)
    {
        public string ErrorMessage = string.Empty;
        public Loan loan = loanService.getById(loanApplicationDTO.LoanId);
        public Account account = accountService.getByNumber(loanApplicationDTO.ToAccountNumber);
        public ClientDTO verifiedClient = client;
        public string LoanApplicationDataVrf()
        {
            //Verificacion de campos del prestamo.
            ClientDTO toClient = clientService.getClientById(account.ClientId);
            if (loan == null) { ErrorMessage = "El prestamo no existe"; }
            else if (loanApplicationDTO.Amount <= 0 || loanApplicationDTO.Amount > loan.MaxAmount) { ErrorMessage = "La cantidad requerida es incorrecta"; }
            else if (loanApplicationDTO.Payments == null || !loan.Payments.Contains(loanApplicationDTO.Payments)) { ErrorMessage = "Las cuotas requeridas son invalidas"; }
            else if (verifiedClient.Id != toClient.Id) { ErrorMessage = "La cuenta destino no pertenece a este cliente"; }
            return ErrorMessage;
        }

        public ClientLoan ClientLoanVerifiedGeneration()
        {
            //Creacion del prestamo.
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
            //Creacion de la transaccion del prestamo.
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