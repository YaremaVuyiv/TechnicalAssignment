using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TechnicalAssignment.ResponseModels;

namespace TechnicalAssignment.Services.Contracts
{
    public interface ITransactionService
    {
        Task<TransactionResponseDataModel> ParseAndValidateTransactionAsync(IFormFile file);
    }
}
