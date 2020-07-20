using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TechnicalAssignment.Models;
using TechnicalAssignment.ResponseModels;

namespace TechnicalAssignment.Services.Contracts
{
    public interface ITransactionService
    {
        Task<TransactionResponseDataModel<TransactionBaseModel>> ParseAndValidateTransactionAsync(IFormFile file);
    }
}
