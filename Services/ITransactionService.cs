using HomeBankingMinHub.Models.DTO;

namespace HomeBankingMinHub.Services
{
    public interface ITransactionService
    {
        void createNewTransaction (NewTransactionsDTO newtransactionDTO);
    }
}
