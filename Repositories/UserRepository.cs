using Bookstore.Context;
using Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using Bookstore.Repositories.Interfaces;

namespace Bookstore.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(BookstoreContext context) : base(context)
    {
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _dbSet
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserWithOrdersAsync(int userId)
    {
        return await _context.Users
            .Include(u => u.Orders)
            .ThenInclude(o => o.Items)
            .ThenInclude(oi => oi.Book)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<IEnumerable<User>> GetAdminUsersAsync()
    {
        return await _dbSet
            .Where(u => u.IsAdmin)
            .ToListAsync();
    }

    public async Task<bool> IsEmailUniqueAsync(string email, int? excludeUserId = null)
    {
        if (excludeUserId.HasValue)
        {
            return !await _dbSet.AnyAsync(u => u.Email == email && u.Id != excludeUserId.Value);
        }
        return !await _dbSet.AnyAsync(u => u.Email == email);
    }

}
