using Microsoft.EntityFrameworkCore;
using TechnicalAssignment.Data.Entities;

namespace TechnicalAssignment.Data
{
    public class TransactionDbContext: DbContext
    {
        public TransactionDbContext(DbContextOptions options): base(options)
        {

        }

        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<TransactionStatus> TransactionStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().HasKey(p => p.Id);
            modelBuilder.Entity<Transaction>().Property(p => p.Id)
                .HasMaxLength(50);
            modelBuilder.Entity<Transaction>().Property(p => p.CurrencyCode)
                .HasMaxLength(3)
                .IsRequired();

            modelBuilder.Entity<TransactionStatus>().HasKey(x => x.Id);
            modelBuilder.Entity<TransactionStatus>().Property(x => x.DisplayName)
                .IsRequired();
            modelBuilder.Entity<TransactionStatus>()
                .HasMany(x => x.Transactions)
                .WithOne(x => x.Status)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
