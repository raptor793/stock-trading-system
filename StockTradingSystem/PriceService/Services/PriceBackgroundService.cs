using MassTransit;
using Shared.Models.Events;

namespace PriceService.Services
{
    public class PriceBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly Dictionary<string, decimal> _stockPrices = new()
        {
            { "AAPL", 170.0M },
            { "TSLA", 700.0M },
            { "NVDA", 950.0M }
        };

        private readonly Random _random = new();

        public PriceBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var ticker in _stockPrices.Keys.ToList())
                {
                    var newPrice = GetRandomPrice(_stockPrices[ticker]);

                    _stockPrices[ticker] = newPrice;

                    var priceEvent = new StockPriceUpdatedEvent
                    {
                        Ticker = ticker,
                        NewPrice = newPrice,
                        Timestamp = DateTime.UtcNow
                    };

                    using (var scope = _serviceScopeFactory.CreateScope()) //Resolving Scoped Di in Singleton service
                    {
                        var pubishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                        await pubishEndpoint.Publish(priceEvent, stoppingToken);
                    }
                }

                await Task.Delay(1000, stoppingToken); // Update every 1 second
            }
        }

        private decimal GetRandomPrice(decimal currentPrice)
        {
            var changePercent = (decimal)(_random.NextDouble() * 0.05 - 0.025); // ±2.5%

            return Math.Round(currentPrice * (1 + changePercent), 2);
        }
    }
}
