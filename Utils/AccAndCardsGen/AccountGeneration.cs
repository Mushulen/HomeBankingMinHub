using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomeBankingMinHub.Utils.AccAndCardsGen
{
    public class AccountGeneration
    {
        public static Account NewAccountGeneration (Client currentClient)
        {
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
