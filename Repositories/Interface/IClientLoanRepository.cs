using HomeBankingMinHub.Models;

namespace HomeBankingMinHub.Repositories.Interface
{
    public interface IClientLoanRepository
    {
        ClientLoan FindById(long id);
        void Save(ClientLoan loan);
    }
}
