using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAssignment.Data.Entities;
using TechnicalAssignment.Data.Repositories.Contracts;

namespace TechnicalAssignment.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext _context;

        public TransactionRepository(TransactionDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(IEnumerable<Transaction> entity)
        {
            await _context.Transactions.AddRangeAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionByDateRangeAsync(DateTime fromDateTime, DateTime toDateTime)
        {
            var entities = await _context.Transactions.Where(x => x.Date >= fromDateTime && x.Date <= toDateTime).ToListAsync();

            return entities;
        }

        public async Task<Transaction> GetTransactionByIdAsync(string id)
        {
            var entity = await _context.Transactions.FindAsync(id);

            return entity;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionByStatusAsync(long status)
        {
            var entities = await _context.Transactions.Where(x => x.StatusId == status).ToListAsync();

            return entities;
        }
    }
}
