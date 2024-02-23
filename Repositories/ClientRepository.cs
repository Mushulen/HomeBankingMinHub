using HomeBankingMinHub.Models;
using HomeBankingMinHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace HomeBankingMinHub.Repositories
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }
        public IEnumerable<Client> GetAllClients()
        {
            return FindAll()
                   .Include(client => client.Accounts)
                   .ThenInclude(tr => tr.Transactions)
                   .Include(client => client.Cards)
                   .Include(client => client.ClientLoans)
                   .ThenInclude(cl => cl.Loan)
                   .ToList();
        }
        public Client FindById(long id)
        {
            return FindByCondition(client => client.Id == id)
                   .Include(client => client.Accounts)
                   .ThenInclude(tr => tr.Transactions)
                   .Include(client => client.Cards)
                   .Include(client => client.ClientLoans)
                   .ThenInclude(cl => cl.Loan)
                   .FirstOrDefault();
        }
        public Client FindByEmail(string email)
        {
            return FindByCondition(client => client.Email.ToUpper() == email.ToUpper())
                    .Include(client => client.Accounts)
                    .ThenInclude(tr => tr.Transactions)
                    .Include(client => client.Cards)
                    .Include(client => client.ClientLoans)
                    .ThenInclude(cl => cl.Loan)
                    .FirstOrDefault();
        }
        public void Save(Client client)
        {
            Create(client);
            SaveChanges();
        }
    }
}