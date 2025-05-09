using MassTransit;
using PortfolioService.Models;
using PortfolioService.Repositories;
using Shared.Models.Events;

namespace PortfolioService.Services.Consumers
{
    public class OrderExecutedConsumer : IConsumer<OrderExecutedEvent>
    {
        private readonly IPortfolioStocksRepository _portfolioStocksRepository;

        public OrderExecutedConsumer(IPortfolioStocksRepository portfolioStocksRepository)
        {
            _portfolioStocksRepository = portfolioStocksRepository;
        }

        public async Task Consume(ConsumeContext<OrderExecutedEvent> context)
        {
            OrderExecutedEvent msg = context.Message;

            var portfolio = await _portfolioStocksRepository.GetPortfolioWithStocksByUserIdAsync(msg.UserId) ?? new Portfolio { UserId = msg.UserId };

            var stock = portfolio.Stocks.FirstOrDefault(s => s.Ticker == msg.Ticker);

            bool hasStocks = portfolio.Stocks.Count > 0;

            if (stock == null)
            {
                stock = new Stock
                {
                    Ticker = msg.Ticker,
                    Quantity = 0,
                    CurrentPrice = msg.Price
                };

                portfolio.Stocks.Add(stock);
            }

            stock.CurrentPrice = msg.Price;

            if (msg.Side == "buy")
            {
                stock.Quantity += msg.Quantity;
            }
            else
            {
                stock.Quantity -= msg.Quantity;
            }

            if (!hasStocks)
            {
                await _portfolioStocksRepository.AddPortfolioWithStocksAsync(portfolio);
            }
            else
            {
                await _portfolioStocksRepository.AddPortfolioStocksAsync(stock);
            }
        }
    }
}
