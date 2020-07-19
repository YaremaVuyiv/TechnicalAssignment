using System.Collections.Generic;

namespace TechnicalAssignment.Data.Entities
{
    public class TransactionStatus
    {
        public long Id { get; set; }

        public string DisplayName { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
