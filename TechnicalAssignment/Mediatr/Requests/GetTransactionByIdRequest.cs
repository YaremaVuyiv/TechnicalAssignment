using MediatR;
using TechnicalAssignment.ResponseModels;

namespace TechnicalAssignment.Mediatr.Requests
{
    public class GetTransactionByIdRequest: IRequest<TransactionResponseDataModel<GetTransactionResponseModel>>
    {
        public string Id { get; set; }
    }
}
