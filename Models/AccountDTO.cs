using HomeBankingMinHub.Models;
using System.Collections.Generic;
using System;

namespace HomeBankingMinHub.dtos

{
    public class AccountDTO
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public DateTime CreationDate { get; set; }
        public double Balance { get; set; }
        public ICollection<TransactionsDTO> Transaction { get; set; }
    }
}