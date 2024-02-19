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
            int CreditCards = 0;
            int DebitCards = 0;
            string ErrorMessage = string.Empty;

            //Validacion si el color y typo de tarjeta existe.
            if (newCard.Type != CardType.CREDIT.ToString().ToUpper() || newCard.Type != CardType.DEBIT.ToString().ToUpper())
            { ErrorMessage = "El tipo de tarjeta que desea crear no existe"; }
            else if (newCard.Color != CardColor.SILVER.ToString().ToUpper() || newCard.Color != CardColor.GOLD.ToString().ToUpper() || newCard.Color != CardColor.TITANIUM.ToString().ToUpper())
            { ErrorMessage += "El color de tarjea que desea crear no existe"; }

            //Se verifica si el cliente ya posee una tarjeta de ese tipo con ese colo o si ya posee 3 tarjetas de un tipo.
            foreach (var card in cards)
            {
                if (card.Type.ToString().ToUpper() == "CREDIT") { CreditCards++; }
                else { DebitCards++; }
                if (card.Type.ToString().ToUpper() == newCard.Type.ToUpper() && card.Color.ToString().ToUpper() == newCard.Color.ToUpper()) { ErrorMessage += "Usted ya posee una tarjeta de este tipo"; }
            }

            if (CreditCards == 3) { ErrorMessage += "Usted no puede tener mas de 3 tarjetas de credito"; }
            else if (DebitCards == 3) { ErrorMessage += "Usted no puede tener mas de 3 tarjetas de debito"; }

            return ErrorMessage;
        }
    }
}