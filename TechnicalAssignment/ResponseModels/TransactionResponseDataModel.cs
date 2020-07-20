using System.Collections.Generic;
using TechnicalAssignment.Models;

namespace TechnicalAssignment.ResponseModels
{
    public class TransactionResponseDataModel : TransactionResponseModel
    {
        public IEnumerable<TransactionModel> Data { get; set; }
    }
}
