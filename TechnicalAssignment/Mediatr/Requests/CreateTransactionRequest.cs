using MediatR;
using Microsoft.AspNetCore.Http;
using TechnicalAssignment.ResponseModels;

namespace TechnicalAssignment.Mediatr.Requests
{
    public class CreateTransactionRequest: IRequest<TransactionResponseModel>
    {
        public IFormFile FileData { get; set; }
    }
}
