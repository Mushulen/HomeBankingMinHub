using HomeBankingMinHub.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HomeBankingMinHub.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }
        public Account FindById(long id)
        {
            return FindByCondition(account => account.Id == id)
                .FirstOrDefault();
        }
        public IEnumerable<Account> GetAllAccounts()
        {
            return FindAll()
                   .ToList();
        }
        public void Save(Account account)
        {
            Create(account);
            SaveChanges();
        }
    }
}