using System;

namespace TechnicalAssignment.Models
{
    public class TransactionBaseModel
    {
        public string Id { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyCode { get; set; }

        public DateTime Date { get; set; }
    }
}
