using Bookstore.Entities;

namespace Bookstore.Repositories.Interfaces;

public interface ICartRepository
{
    Task<Cart> GetCartByUserIdAsync(int userId);
    Task AddItemAsync(int userId, int bookId, int quantity = 1);
    Task RemoveItemAsync(int userId, int bookId);
    Task UpdateQuantityAsync(int userId, int bookId, int quantity);
    Task ClearCartAsync(int userId);
    Task<int> GetCartItemCountAsync(int userId);
}
