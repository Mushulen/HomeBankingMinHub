using HomeBankingMinHub.Models;

namespace HomeBankingMinHub.Utils.AccAndCardsGen
{
    public class AccountGeneration
    {
        public static Account NewAccountGeneration (Client currentClient)
        {

            //Generador de numeros aleatorios para las cuentas.
            Random random = new Random();
            string accLetters = new string (currentClient.FirstName.Take(3).ToArray());
            string randomNumbers = new string(Enumerable.Range(0, 8).Select(_ => random.Next(10).ToString()[0]).ToArray());

            var account = new Account()
            {
                ClientId = currentClient.Id,
                Number = accLetters.ToUpper() + "-" + randomNumbers,
                CreationDate = DateTime.Now,
                Balance = 0,
            };
            return account;
        }
    }
}