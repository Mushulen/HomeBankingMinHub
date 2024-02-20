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
        public void Save(ClientLoan clientLoan)
        {
            Create(clientLoan);
            SaveChanges();
        }
    }
}