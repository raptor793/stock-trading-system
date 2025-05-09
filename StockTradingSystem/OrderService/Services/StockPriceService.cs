namespace OrderService.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly Dictionary<string, decimal> _mockPrices = new()
        {
            { "AAPL", 170.25M },
            { "TSLA", 750.00M },
            { "NVDA", 950.10M }
        };

        public Task<decimal> GetLatestPriceAsync(string ticker)
        {
            return Task.FromResult(_mockPrices.TryGetValue(ticker, out var price) ? price : 100M);
        }
    }
}
