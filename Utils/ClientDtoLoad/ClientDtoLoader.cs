using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;

namespace HomeBankingMinHub.Utils.ClientDtoLoad
{
    public class ClientDtoLoader
    {
        public static ClientDTO CurrentClient (Client currentclient)
        {
            var CurrentClient = new ClientDTO
            {
                Id = currentclient.Id,
                Email = currentclient.Email,
                FirstName = currentclient.FirstName,
                LastName = currentclient.LastName,
                Accounts = currentclient.Accounts.Select(ac => new AccountDTO
                {
                    Id = ac.Id,
                    Balance = ac.Balance,
                    CreationDate = ac.CreationDate,
                    Number = ac.Number
                }).ToList(),
                Credits = currentclient.ClientLoans.Select(cl => new ClientLoanDTO
                {
                    Id = cl.Id,
                    LoanId = cl.LoanId,
                    Name = cl.Loan.Name,
                    Amount = cl.Amount,
                    Payments = cl.Payments
                }).ToList(),
                Cards = currentclient.Cards.Select(c => new CardDTO
                {
                    Id = c.Id,
                    CardHolder = c.CardHolder,
                    Color = c.Color.ToString(),
                    Type = c.Type.ToString(),
                    Cvv = c.Cvv,
                    FromDate = c.FromDate,
                    Number = c.Number,
                    ThruDate = c.ThruDate,
                }).ToList()
            };
            return (CurrentClient);
        }
    }
}
