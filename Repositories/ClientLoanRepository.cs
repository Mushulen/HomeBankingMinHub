using HomeBankingMinHub.Models;
using HomeBankingMinHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace HomeBankingMinHub.Repositories
{
    public class ClientLoanRepository : RepositoryBase<ClientLoan>, IClientLoanRepository
    {
        public ClientLoanRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }
        public ClientLoan FindById(long id)
        {
            return FindByCondition(loan => loan.Id == id)
                    .FirstOrDefault();
        }
        public void Save(ClientLoan clientloan)
        {
            Create(clientloan);
            SaveChanges();
        }
    }
}
