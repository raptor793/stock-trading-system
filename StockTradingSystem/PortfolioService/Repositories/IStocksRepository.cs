namespace PortfolioService.Repositories
{
    public interface IStocksRepository
    {
        Task UpdateStockPriceByTickerAsync(string ticker, decimal newPrice);
    }
}
