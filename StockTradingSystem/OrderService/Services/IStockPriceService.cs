namespace OrderService.Services
{
    public interface IStockPriceService
    {
        Task<decimal> GetLatestPriceAsync(string ticker);
    }
}
