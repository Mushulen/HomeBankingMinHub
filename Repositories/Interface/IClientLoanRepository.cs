using HomeBankingMinHub.Models;

namespace HomeBankingMinHub.Repositories.Interface
{
    public interface IClientLoanRepository
    {
        void Save(ClientLoan clientLoan);
    }
}