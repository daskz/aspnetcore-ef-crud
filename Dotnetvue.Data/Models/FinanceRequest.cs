using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Dotnetvue.Data.Models
{
    public class FinanceRequest
    {
        public Guid? Id { get; set; }
        public string PublicNumber { get; set; }

        public string Purpose { get; set; }

        public int MonthCount { get; set; }

        [Range(1, 100)]
        public int InterestRate { get; set; }

        public decimal Amount { get; set; }

        [JsonIgnore]
        public User Author { get; set; }
    }
}
