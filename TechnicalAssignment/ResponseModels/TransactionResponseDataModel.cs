using System.Collections.Generic;

namespace TechnicalAssignment.ResponseModels
{
    public class TransactionResponseDataModel<T> : TransactionResponseModel
    {
        public IEnumerable<T> Data { get; set; }
    }
}
