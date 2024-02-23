namespace HomeBankingMinHub.Models.DTO
{
    public class ClientLoanDTO
    {
        public long Id { get; set; }
        public double Amount { get; set; }
        public string Payments { get; set; }
        public long ClientId { get; set; }
        public long LoanId { get; set; }
        public string Name { get; set; }

        public ClientLoanDTO(ClientLoan clientLoan)
        {
            Id = clientLoan.Id;
            LoanId = clientLoan.LoanId;
            Name = clientLoan.Loan.Name;
            Amount = clientLoan.Amount;
            Payments = clientLoan.Payments;
        }
    }
}