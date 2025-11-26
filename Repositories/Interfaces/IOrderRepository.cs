using Bookstore.Entities;

namespace Bookstore.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<Order> CreateOrderAsync(Order order);
    Task<List<Order>> GetOrdersByUserIdAsync(int userId);
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<List<Order>> GetAllOrdersAsync();
    Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
}
