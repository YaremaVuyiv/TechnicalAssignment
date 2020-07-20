using MediatR;
using TechnicalAssignment.ResponseModels;

namespace TechnicalAssignment.Mediatr.Requests
{
    public class GetTransactionByStatusRequest : IRequest<TransactionResponseDataModel<GetTransactionResponseModel>>
    {
        public string Status { get; set; }
    }
}
