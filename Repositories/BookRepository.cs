using Bookstore.Context;
using Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using Bookstore.Repositories.Interfaces;

namespace Bookstore.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    public BookRepository(BookstoreContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId)
    {
        return await _context.Books
            .Include(b => b.BookCategories)
            .ThenInclude(bc => bc.Category)
            .Where(b => b.BookCategories.Any(bc => bc.CategoryId == categoryId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author)
    {
        return await _dbSet
            .Where(b => b.Author.Contains(author))
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm)
    {
        return await _dbSet
            .Where(b => b.Title.Contains(searchTerm) || 
                       b.Author.Contains(searchTerm) || 
                       b.Description.Contains(searchTerm))
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
    {
        return await _dbSet
            .Where(b => b.CopiesAvailable > 0)
            .ToListAsync();
    }

    public async Task<Book?> GetBookWithCategoriesAsync(int id)
    {
        return await _context.Books
            .Include(b => b.BookCategories)
            .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<bool> IsBookAvailableAsync(int bookId, int quantity)
    {
        var book = await _dbSet.FindAsync(bookId);
        return book != null && book.CopiesAvailable >= quantity;
    }
}
