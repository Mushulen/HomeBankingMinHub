using HomeBankingMindHub.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HomeBankingMinHub.Models
{
    public class DbInitializer
    {
        public static void Initialize(HomeBankingContext context)
        {
            if (!context.Clients.Any())
            {
                var clients = new Client[]
                {
                    new Client {FirstName="Leandro", LastName="Rodriguez", Email = "lrodriguez@gmail.com", Password="qwerty"},

                    new Client {FirstName="Lautaro", LastName="Lucci", Email = "llucci@gmail.com", Password="asdfg"},

                    new Client {FirstName="Gimena", LastName="Dali", Email = "gdali@gmail.com", Password="qzxcvb"}
                };
                context.Clients.AddRange(clients);
                context.SaveChanges();
            }
            if (!context.Account.Any())
            {
                var accountLeandro = context.Clients.FirstOrDefault(c => c.Email == "lrodriguez@gmail.com");
                if (accountLeandro != null)
                {
                    var accounts = new Account[]
                    {
                        new Account {ClientId = accountLeandro.Id, CreationDate = DateTime.Now, Number = "LEA001", Balance = 0 },

                        new Account {ClientId = accountLeandro.Id, CreationDate = DateTime.Now, Number = "LEA002", Balance = 0 },

                        new Account {ClientId = accountLeandro.Id, CreationDate = DateTime.Now, Number = "LEA003", Balance = 0 },
                    };
                    context.Account.AddRange(accounts);
                    context.SaveChanges();
                }
            }
            if (!context.Transactions.Any())
            {
                var account1 = context.Account.FirstOrDefault(c => c.Number == "LEA001");
                if (account1 != null)
                {
                    var transactions = new Transactions[]
                    {
                        new Transactions { AccountId= account1.Id, Amount = 10000, Date= DateTime.Now.AddHours(-5), Description = "Transferencia reccibida", Type = TransactionType.CREDIT.ToString() },

                        new Transactions { AccountId= account1.Id, Amount = -2000, Date= DateTime.Now.AddHours(-6), Description = "Compra en tienda mercado libre", Type = TransactionType.DEBIT.ToString() },

                        new Transactions { AccountId= account1.Id, Amount = -3000, Date= DateTime.Now.AddHours(-7), Description = "Compra en tienda xxxx", Type = TransactionType.DEBIT.ToString() },
                    };
                    context.Transactions.AddRange(transactions);
                    context.SaveChanges();
                }
            }
            ModifyAccBalance(context);
        }
        public static void ModifyAccBalance(HomeBankingContext context)
        {
            foreach (Transactions transactions in context.Transactions.ToList())
            {
                var account = context.Account.FirstOrDefault(c => c.Id == transactions.AccountId);
                account.SetBalance(transactions.Amount);
            }
            context.SaveChanges();
        }
    }
}
