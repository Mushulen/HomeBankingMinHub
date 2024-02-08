using HomeBankingMinHub.Models.Enums;
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
            if (!context.Cards.Any())
            {
                var client1 = context.Clients.FirstOrDefault(c => c.Email == "lrodriguez@gmail.com");
                if (client1 != null)
                {
                    var cards = new Card[]
                    {
                        new Card
                        {
                            ClientId = client1.Id,
                            CardHolder = client1.FirstName + " " + client1.LastName,
                            Type = CardType.DEBIT.ToString(),
                            Color = CardColor.GOLD.ToString(),
                            Number = "3325-6745-7876-4445",
                            Cvv = 990,
                            FromDate = DateTime.Now,
                            ThruDate = DateTime.Now.AddYears(4),
                        },
                        new Card
                        {
                            ClientId = client1.Id,
                            CardHolder = client1.FirstName + " " + client1.LastName,
                            Type = CardType.CREDIT.ToString(),
                            Color = CardColor.TITANIUM.ToString(),
                            Number = "2234-6745-552-7888",
                            Cvv = 750,
                            FromDate = DateTime.Now,
                            ThruDate = DateTime.Now.AddYears(5),
                        },
                    };
                    foreach (Card card in cards)
                    {
                        context.Cards.Add(card);
                    }
                    context.SaveChanges();
                }
            }
            if (!context.Loans.Any())
            {
                var loans = new Loan[]
                {
                    new Loan { Name = "Hipotecario", MaxAmount = 500000, Payments = "12,24,36,48,60" },
                    new Loan { Name = "Personal", MaxAmount = 100000, Payments = "6,12,24" },
                    new Loan { Name = "Automotriz", MaxAmount = 300000, Payments = "6,12,24,36" },
                    new Loan { Name = "Prestario", MaxAmount = 250000, Payments = "6,12,24"},
                    new Loan { Name = "Prendario", MaxAmount = 350000, Payments = "6,12,24,36"},
                };
                foreach (Loan loan in loans)
                {
                    context.Loans.Add(loan);
                }
                context.SaveChanges();
                var client1 = context.Clients.FirstOrDefault(c => c.Email == "lrodriguez@gmail.com");
                if (client1 != null)
                {
                    var loan1 = context.Loans.FirstOrDefault(l => l.Name == "Hipotecario");
                    if (loan1 != null)
                    {
                        var clientLoan1 = new ClientLoan
                        {
                            Amount = 400000,
                            ClientId = client1.Id,
                            LoanId = loan1.Id,
                            Payments = "60"
                        };
                        context.ClientLoans.Add(clientLoan1);
                    }
                    var loan2 = context.Loans.FirstOrDefault(l => l.Name == "Personal");
                    if (loan2 != null)
                    {
                        var clientLoan2 = new ClientLoan
                        {
                            Amount = 50000,
                            ClientId = client1.Id,
                            LoanId = loan2.Id,
                            Payments = "12"
                        };
                        context.ClientLoans.Add(clientLoan2);
                    }
                    var loan3 = context.Loans.FirstOrDefault(l => l.Name == "Automotriz");
                    if (loan3 != null)
                    {
                        var clientLoan3 = new ClientLoan
                        {
                            Amount = 100000,
                            ClientId = client1.Id,
                            LoanId = loan3.Id,
                            Payments = "24"
                        };
                        context.ClientLoans.Add(clientLoan3);
                    }
                    var loan4 = context.Loans.FirstOrDefault(l => l.Name == "Prestario");
                    if (loan4 != null)
                    {
                        var clientLoan4 = new ClientLoan
                        {
                            Amount = 180000,
                            ClientId = client1.Id,
                            LoanId = loan4.Id,
                            Payments = "24"
                        };
                        context.ClientLoans.Add(clientLoan4);
                    }
                    context.SaveChanges();
                }
            }

            //Metodo que actualiza el balance de la cuenta
            ModifyAccBalance(context);
        }
        public static void ModifyAccBalance(HomeBankingContext context)
        {
            foreach (Transactions transactions in context.Transactions.ToList())
            {
                var account = context.Account.FirstOrDefault(ac => ac.Id == transactions.AccountId);
                account.SetBalance(transactions.Amount);
            }
            context.SaveChanges();
        }
    }
}
