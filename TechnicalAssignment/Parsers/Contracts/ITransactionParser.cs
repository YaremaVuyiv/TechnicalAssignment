using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalAssignment.Models;

namespace TechnicalAssignment.Parsers.Contracts
{
    public interface ITransactionParser
    {
        Task<IEnumerable<TransactionModel>> Parse(IFormFile file);
    }
}
