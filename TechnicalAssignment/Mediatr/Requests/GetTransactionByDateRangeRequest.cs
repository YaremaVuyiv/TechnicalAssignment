using MediatR;
using System;
using TechnicalAssignment.ResponseModels;

namespace TechnicalAssignment.Mediatr.Requests
{
    public class GetTransactionByDateRangeRequest: IRequest<TransactionResponseDataModel<GetTransactionResponseModel>>
    {
        public DateTime FromDateTime { get; set; }

        public DateTime ToDateTime { get; set; }
    }
}
