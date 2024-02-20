using HomeBankingMinHub.Models;

namespace HomeBankingMinHub.Repositories.Interface
{
    public interface ILoanRepository
    {
        IEnumerable<Loan> GetAllLoans();
        Loan FindById(long id);
    }
}