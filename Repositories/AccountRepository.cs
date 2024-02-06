﻿using HomeBankingMinHub.Models;
using HomeBankingMinHub.Repositories.Interface;
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
                    .Include(account => account.Transaction)
                    .FirstOrDefault();
        }
        public IEnumerable<Account> GetAllAccounts()
        {
            return FindAll()
                    .Include(account => account.Transaction)
                    .ToList();
        }
        public void Save(Account account)
        {
            Create(account);
            SaveChanges();
        }
    }
}