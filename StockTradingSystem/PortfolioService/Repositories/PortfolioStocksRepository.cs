using Microsoft.EntityFrameworkCore;
using PortfolioService.Data;
using PortfolioService.Models;

namespace PortfolioService.Repositories
{
    public class PortfolioStocksRepository : IPortfolioStocksRepository
    {
        private readonly PortfolioDbContext _dbContext;

        public PortfolioStocksRepository(PortfolioDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Portfolio?> GetPortfolioWithStocksByUserIdAsync(string userId)
        {
            return await _dbContext.Portfolios
                .Include(p => p.Stocks)
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task AddPortfolioWithStocksAsync(Portfolio portfolio)
        {
            using (var database = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _dbContext.Add(portfolio);
                    await _dbContext.SaveChangesAsync();

                    await database.CommitAsync();
                }
                catch (Exception)
                {
                    await database.RollbackAsync();

                    throw;
                }
            }
        }

        public async Task AddPortfolioStocksAsync(Stock stock)
        {
            using (var database = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var entry = _dbContext.Entry(stock);

                    switch (entry.State)
                    {
                        case EntityState.Detached:
                        case EntityState.Added:
                            _dbContext.Stocks.Add(stock);
                            break;
                        case EntityState.Modified:
                            _dbContext.Stocks.Update(stock);
                            break;
                        default:
                            break;
                    }

                    await _dbContext.SaveChangesAsync();

                    await database.CommitAsync();
                }
                catch (Exception)
                {
                    await database.RollbackAsync();

                    throw;
                }
            }
        }
    }
}
