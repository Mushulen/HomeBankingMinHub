using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;

namespace HomeBankingMinHub.Utils.DtoLoad
{
    public class LoanDtoLoader
    {
        public static List<LoanDTO> AllLoans(IEnumerable<Loan> loans)
        {
            var loansDTO = new List<LoanDTO>();
            foreach (Loan loan in loans)
            {
                var newLoanDTO = new LoanDTO
                {
                    Id = loan.Id,
                    Name = loan.Name,
                    MaxAmount = loan.MaxAmount,
                    Payments = loan.Payments,
                };
                loansDTO.Add(newLoanDTO);
            }
            return loansDTO;
        }
    }
}