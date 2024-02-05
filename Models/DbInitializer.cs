using HomeBankingMindHub.Models;

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
                    new Client {FirstName="Leandro", LastName="Rodriguez", Email = "lrodriguez@gmail.com", Password="qwerty"}
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
                        new Account {ClientId = accountLeandro.Id, CreationDate = DateTime.Now, Number = string.Empty, Balance = 0 }
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
                    foreach (Transactions transaction in transactions)
                    {
                        context.Transactions.Add(transaction);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
