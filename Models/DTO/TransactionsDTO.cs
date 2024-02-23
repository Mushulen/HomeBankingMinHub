namespace HomeBankingMinHub.Models.DTO
{
    public class TransactionsDTO
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public TransactionsDTO(Transactions transactions)
        {
            Id = transactions.Id;
            AccountId = transactions.AccountId;
            Type = transactions.Type.ToString();
            Amount = transactions.Amount;
            Description = transactions.Description;
            Date = transactions.Date;
        }
    }
}