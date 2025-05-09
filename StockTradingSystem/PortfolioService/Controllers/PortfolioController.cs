using Microsoft.AspNetCore.Mvc;
using PortfolioService.Repositories;

namespace PortfolioService.Controllers
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioStocksRepository _repo;
        private readonly ILogger<PortfolioController> _logger;

        public PortfolioController(IPortfolioStocksRepository repo, ILogger<PortfolioController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPortfolio(string userId)
        {
            var portfolio = await _repo.GetPortfolioWithStocksByUserIdAsync(userId);

            if (portfolio == null)
            {
                return NotFound();
            }

            var totalValue = portfolio.Stocks.Sum(s => s.Quantity > 0 ? s.Value : 0);

            return Ok(new { UserId = portfolio.UserId, TotalValue = totalValue });
        }
    }
}
