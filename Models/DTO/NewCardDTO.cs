using HomeBankingMinHub.Models.Enums;

namespace HomeBankingMinHub.Models.DTO
{
    public class NewCardDTO
    {
        public CardType cardType { get; set; }
        public CardColor cardColor { get; set; }
    }
}
