using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalAssignment.Data.Entities;

namespace TechnicalAssignment.Data.Repositories.Contracts
{
    public interface ITransactionRepository
    {
        Task CreateAsync(IEnumerable<Transaction> entity);

        Task<Transaction> GetTransactionByIdAsync(string id);

        Task<IEnumerable<Transaction>> GetTransactionByStatusAsync(long status);

        Task<IEnumerable<Transaction>> GetTransactionByDateRangeAsync(DateTime fromDateTime, DateTime toDateTime);
    }
}
