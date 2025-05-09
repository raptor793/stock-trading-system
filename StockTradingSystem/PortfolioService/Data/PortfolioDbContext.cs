using Microsoft.EntityFrameworkCore;
using PortfolioService.Data.Configurations;
using PortfolioService.Models;

namespace PortfolioService.Data
{
    public class PortfolioDbContext : DbContext
    {
        public DbSet<Portfolio> Portfolios => Set<Portfolio>();
        public DbSet<Stock> Stocks => Set<Stock>();

        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new PortfolioConfiguration());
        }
    }
}
