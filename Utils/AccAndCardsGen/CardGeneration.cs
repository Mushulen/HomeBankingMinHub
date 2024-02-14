using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Models.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomeBankingMinHub.Utils.AccAndCardsGen
{
    public class CardGeneration
    {
        public static Card NewCardGeneration(Client currentClient, NewCardDTO newCard)
        {
            Random random = new Random();
            string randomNumbers = new string(Enumerable.Range(0, 16).Select(_ => random.Next(10).ToString()[0]).ToArray());
            int randomCvv = random.Next(100, 1000);

            var account = new Card
            {
                ClientId = currentClient.Id,
                CardHolder = currentClient.FirstName + " " + currentClient.LastName,
                Type = newCard.cardType,
                Color = newCard.cardColor,
                Number = InsertDashes(randomNumbers,4),
                Cvv = randomCvv,
                FromDate = DateTime.Now,
                ThruDate = DateTime.Now.AddYears(4),
            };
            return account;
        }
        private static string InsertDashes(string randomNumbers, int interval)
        {
            string result = "";

            for (int i = 0; i < randomNumbers.Length; i += interval)
            {
                int remainingLength = Math.Min(interval, randomNumbers.Length - i);
                result += randomNumbers.Substring(i, remainingLength);
                if (i + remainingLength < randomNumbers.Length)
                {
                    result += "-";
                }
            }

            return result;
        }
    }
}
