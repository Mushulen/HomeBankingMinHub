using HomeBankingMinHub.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HomeBankingMinHub.Models.DTO
{
    public class ClientLoanDTO
    {
        [JsonIgnore]
        public long Id { get; set; }
        public double Amount { get; set; }
        public string Payments { get; set; }
        public long ClientId { get; set; }
        public long LoanId { get; set; }
        public string Name { get; set; }
    }
}
