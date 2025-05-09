using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
    }
}
