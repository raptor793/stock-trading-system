using MassTransit;
using PortfolioService.Repositories;
using Shared.Models.Events;

namespace PortfolioService.Services.Consumers
{
    public class StockPriceUpdatedConsumer : IConsumer<StockPriceUpdatedEvent>
    {
        private readonly IStocksRepository _stocksRepository;

        public StockPriceUpdatedConsumer(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        public async Task Consume(ConsumeContext<StockPriceUpdatedEvent> context)
        {
            StockPriceUpdatedEvent msg = context.Message;

            await _stocksRepository.UpdateStockPriceByTickerAsync(msg.Ticker, msg.NewPrice);
        }
    }
}
