using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Models.Enums;
using HomeBankingMinHub.Repositories.Interface;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace HomeBankingMinHub.Utils.AccAndCardsGen
{
    public class CardCreationVrf()
    {
        public static string CardsVerification(IEnumerable<Card> cards, NewCardDTO newCard)
        {
            string ErrorMessage = string.Empty;
            string[] CardTypes = new string[] { "CREDIT", "DEBIT" };
            string[] CardColors = new string[] { "SILVER", "GOLD", "TITANIUM" };

            //Validacion si el color y typo de tarjeta existe.
            if (!Array.Exists(CardTypes, element => element == newCard.Type.ToUpper()))
            { ErrorMessage = " El tipo de tarjeta que desea crear no existe"; }
            else if (!Array.Exists(CardColors, element => element == newCard.Color.ToUpper()))
            { ErrorMessage += " El color de tarjea que desea crear no existe"; }

            //Se verifica si el cliente ya posee una tarjeta de ese tipo con ese color o si ya posee 3 tarjetas de un tipo.
            foreach (var card in cards)
            {
                if (card.Type.ToString().ToUpper() == newCard.Type.ToUpper() && card.Color.ToString().ToUpper() == newCard.Color.ToUpper()) { ErrorMessage += " Usted ya posee una tarjeta de este tipo"; }
            }

            return ErrorMessage;
        }
    }
}