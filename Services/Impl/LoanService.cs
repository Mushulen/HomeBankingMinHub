using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Repositories;
using HomeBankingMinHub.Repositories.Interface;
using HomeBankingMinHub.Utils;
using HomeBankingMinHub.Utils.ClientLoanVrf;

namespace HomeBankingMinHub.Services.Impl
{
    public class LoanService(IAccountRepository accountRepository, ITransactionsRepository transactionsRepository, IClientLoanRepository clientLoanRepository, ILoanRepository loanRepository) : ILoanService
    {
        public List<LoanDTO> getAllLoans()
        {
            var loans = loanRepository.GetAllLoans();
            var loansDTO = new List<LoanDTO>();
            foreach (var loan in loans)
            {
                LoanDTO loanDTO = new LoanDTO(loan);
                loansDTO.Add(loanDTO);
            }
            return loansDTO;
        }
        public Loan getById(long id)
        {
            return loanRepository.FindById(id);
        }
        public void createNewClientLoan(LoanApplicationVrf loanApplicationData, LoanApplicationDTO loanApplicationDTO)
        {
            //Creacion del prestamo, la transaccion, y la actualizacion del balance nuevo en la cuenta destino.
            var toAccount = accountRepository.FindByNumber(loanApplicationDTO.ToAccountNumber);

            clientLoanRepository.Save(loanApplicationData.ClientLoanVerifiedGeneration());
            transactionsRepository.Save(loanApplicationData.LoanTransactionGeneration());
            accountRepository.Save(TransactionVerify.BalanceUpdate(toAccount, loanApplicationDTO.Amount));
        }
    }
}