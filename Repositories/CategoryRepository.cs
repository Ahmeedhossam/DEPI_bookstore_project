using Bookstore.Context;
using Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using Bookstore.Repositories.Interfaces;

namespace Bookstore.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(BookstoreContext context) : base(context)
    {
    }

    public async Task<Category?> GetCategoryWithBooksAsync(int categoryId)
    {
        return await _context.Categories
            .Include(c => c.BookCategories)
            .ThenInclude(bc => bc.Book)
            .FirstOrDefaultAsync(c => c.Id == categoryId);
    }

    public async Task<IEnumerable<Category>> GetCategoriesWithBookCountAsync()
    {
        return await _context.Categories
            .Include(c => c.BookCategories)
            .ToListAsync();
    }

    public async Task<Category?> GetCategoryByNameAsync(string name)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.Name == name);
    }
}
