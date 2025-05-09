using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Repositories;
using OrderService.Services;
using Shared.Models.Events;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IStockPriceService _priceService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderRepository orderRepo, IStockPriceService priceService,
            IPublishEndpoint publishEndpoint, ILogger<OrderController> logger)
        {
            _orderRepo = orderRepo;
            _priceService = priceService;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        [HttpPost("add/{userId}")]
        public async Task<IActionResult> AddOrder(string userId, [FromBody] OrderRequest request)
        {
            var price = await _priceService.GetLatestPriceAsync(request.Ticker);

            var order = new Order
            {
                UserId = userId,
                Ticker = request.Ticker,
                Quantity = request.Quantity,
                Side = request.Side.ToLower(),
                Price = price
            };

            await _orderRepo.AddOrderAsync(order);

            var orderEvent = new OrderExecutedEvent
            {
                OrderId = order.Id,
                UserId = userId,
                Ticker = order.Ticker,
                Quantity = order.Quantity,
                Side = order.Side,
                Price = order.Price,
                CreatedAt = order.CreatedAt
            };

            await _publishEndpoint.Publish(orderEvent);

            return Ok(order);
        }
    }
}
