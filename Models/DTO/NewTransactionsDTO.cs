﻿namespace HomeBankingMinHub.Models.DTO
{
    public class NewTransactionsDTO
    {
        public string FromAccountNumber { get; set; }
        public string ToAccountNumber { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}