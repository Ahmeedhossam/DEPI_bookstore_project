using Bookstore.Context;
using Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using Bookstore.Repositories.Interfaces;

namespace Bookstore.Repositories;

public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(BookstoreContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OrderItem>> GetItemsByOrderAsync(int orderId)
    {
        return await _context.OrderItems
            .Include(oi => oi.Book)
            .Where(oi => oi.OrderId == orderId)
            .ToListAsync();
    }

    public async Task<IEnumerable<OrderItem>> GetItemsByBookAsync(int bookId)
    {
        return await _context.OrderItems
            .Include(oi => oi.Order)
            .Where(oi => oi.BookId == bookId)
            .ToListAsync();
    }

    public async Task<OrderItem?> GetOrderItemWithDetailsAsync(int orderItemId)
    {
        return await _context.OrderItems
            .Include(oi => oi.Book)
            .Include(oi => oi.Order)
            .ThenInclude(o => o.User)
            .FirstOrDefaultAsync(oi => oi.Id == orderItemId);
    }
}
