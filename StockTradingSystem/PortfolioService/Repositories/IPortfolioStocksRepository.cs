using PortfolioService.Models;

namespace PortfolioService.Repositories
{
    public interface IPortfolioStocksRepository
    {
        Task<Portfolio?> GetPortfolioWithStocksByUserIdAsync(string userId);
        Task AddPortfolioWithStocksAsync(Portfolio portfolio);
        Task AddPortfolioStocksAsync(Stock stock);
    }
}
