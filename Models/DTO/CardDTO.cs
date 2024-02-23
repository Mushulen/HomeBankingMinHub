using HomeBankingMinHub.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HomeBankingMinHub.Models.DTO
{
    public class CardDTO
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string CardHolder { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Number { get; set; }
        public int Cvv { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ThruDate { get; set; }

        public CardDTO(Card card)
        {
            Id = card.Id;
            CardHolder = card.CardHolder;
            Color = card.Color.ToString();
            Type = card.Type.ToString();
            Cvv = card.Cvv;
            FromDate = card.FromDate;
            Number = card.Number;
            ThruDate = card.ThruDate;
        }
    }
}