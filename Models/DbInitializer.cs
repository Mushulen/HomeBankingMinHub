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
        }
    }
}
