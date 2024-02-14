using HomeBankingMinHub.Models;

namespace HomeBankingMinHub.Repositories.Interface
{
    public interface IClientLoanRepository
    {
        void Save(ClientLoan loan);
        ClientLoan FindById(long id);
    }
}
