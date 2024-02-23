﻿using System.Text.Json.Serialization;

namespace HomeBankingMinHub.Models.DTO
{
    public class NewClientDTO
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}