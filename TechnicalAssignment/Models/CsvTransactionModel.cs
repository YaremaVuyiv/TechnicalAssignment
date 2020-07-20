namespace TechnicalAssignment.Models
{
    public class CsvTransactionModel: TransactionBaseModel
    {
        public CsvTransactionStatuses Status { get; set; }
    }
}
