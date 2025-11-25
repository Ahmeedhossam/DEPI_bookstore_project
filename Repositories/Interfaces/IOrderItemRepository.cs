using Bookstore.Entities;

namespace Bookstore.Repositories.Interfaces;

public interface IOrderItemRepository : IRepository<OrderItem>
{
    Task<IEnumerable<OrderItem>> GetItemsByOrderAsync(int orderId);
    Task<IEnumerable<OrderItem>> GetItemsByBookAsync(int bookId);
    Task<OrderItem?> GetOrderItemWithDetailsAsync(int orderItemId);
}
