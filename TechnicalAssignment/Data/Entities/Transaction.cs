using System;
using TechnicalAssignment.Models;

namespace TechnicalAssignment.Data.Entities
{
    public class Transaction
    {
        public string Id { get; set; }

        public decimal Amount { get; set; }
        
        public string CurrencyCode { get; set; }

        public DateTime Date { get; set; }

        public virtual TransactionStatus Status { get; set; }

        public long StatusId { get; set; }
    }
}
