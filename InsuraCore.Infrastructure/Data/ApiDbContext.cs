using InsuraCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsuraCore.Infrastructure.Data
{
    public class InsuraCoreDbContext : DbContext
    {
        public InsuraCoreDbContext(DbContextOptions<InsuraCoreDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
} 