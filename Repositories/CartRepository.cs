using Bookstore.Context;
using Bookstore.Entities;
using Bookstore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Repositories;

public class CartRepository : ICartRepository
{
    private readonly BookstoreContext _context;

    public CartRepository(BookstoreContext context)
    {
        _context = context;
    }

    public async Task<Cart> GetCartByUserIdAsync(int userId)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Book)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task AddItemAsync(int userId, int bookId, int quantity = 1)
    {
        var cart = await GetCartByUserIdAsync(userId);
        var cartItem = cart.Items.FirstOrDefault(i => i.BookId == bookId);

        if (cartItem != null)
        {
            cartItem.Quantity += quantity;
        }
        else
        {
            cartItem = new CartItem
            {
                CartId = cart.Id,
                BookId = bookId,
                Quantity = quantity
            };
            _context.CartItems.Add(cartItem);
        }

        await _context.SaveChangesAsync();
    }

    public async Task RemoveItemAsync(int userId, int bookId)
    {
        var cart = await GetCartByUserIdAsync(userId);
        var cartItem = cart.Items.FirstOrDefault(i => i.BookId == bookId);

        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateQuantityAsync(int userId, int bookId, int quantity)
    {
        if (quantity <= 0)
        {
            await RemoveItemAsync(userId, bookId);
            return;
        }

        var cart = await GetCartByUserIdAsync(userId);
        var cartItem = cart.Items.FirstOrDefault(i => i.BookId == bookId);

        if (cartItem != null)
        {
            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();
        }
    }

    public async Task ClearCartAsync(int userId)
    {
        var cart = await GetCartByUserIdAsync(userId);
        _context.CartItems.RemoveRange(cart.Items);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetCartItemCountAsync(int userId)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        return cart?.Items.Sum(i => i.Quantity) ?? 0;
    }
}
