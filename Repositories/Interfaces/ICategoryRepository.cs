using Bookstore.Entities;

namespace Bookstore.Repositories.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetCategoryWithBooksAsync(int categoryId);
    Task<IEnumerable<Category>> GetCategoriesWithBookCountAsync();
    Task<Category?> GetCategoryByNameAsync(string name);
}
