﻿using System.Collections.Generic;
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
            await _context.AddRangeAsync(entity);

            await _context.SaveChangesAsync();
        }
    }
}
