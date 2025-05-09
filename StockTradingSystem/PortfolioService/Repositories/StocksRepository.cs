using PortfolioService.Data;

namespace PortfolioService.Repositories
{
    public class StocksRepository : IStocksRepository
    {
        private readonly PortfolioDbContext _dbContext;

        public StocksRepository(PortfolioDbContext context)
        {
            _dbContext = context;
        }

        public async Task UpdateStockPriceByTickerAsync(string ticker, decimal newPrice)
        {
            var stocks = _dbContext.Stocks.Where(s => s.Ticker == ticker);

            foreach (var stock in stocks)
            {
                stock.CurrentPrice = newPrice;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
