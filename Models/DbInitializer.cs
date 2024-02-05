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
        }
    }
}
