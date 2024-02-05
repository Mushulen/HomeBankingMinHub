using HomeBankingMinHub.Models;

namespace HomeBankingMinHub.dtos
{
    public class TransactionsDTO
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
