using Bookstore.Entities;

namespace Bookstore.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserWithOrdersAsync(int userId);
    Task<IEnumerable<User>> GetAdminUsersAsync();
    Task<bool> IsEmailUniqueAsync(string email, int? excludeUserId = null);
}
