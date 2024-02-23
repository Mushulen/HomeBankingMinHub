using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Utils.ClientLoanVrf;

namespace HomeBankingMinHub.Services
{
    public interface ILoanService
    {
        List<LoanDTO> getAllLoans();
        Loan getById(long id);
        void createNewClientLoan(LoanApplicationVrf loanApplicationData, LoanApplicationDTO loanApplicationDTO);
    }
}