using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalAssignment.Data.Entities;

namespace TechnicalAssignment.Data.Repositories.Contracts
{
    public interface ITransactionRepository
    {
        Task CreateAsync(IEnumerable<Transaction> entity);
    }
}
