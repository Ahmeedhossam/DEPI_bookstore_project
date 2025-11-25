using Bookstore.Entities;

namespace Bookstore.Repositories.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId);
    Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author);
    Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm);
    Task<IEnumerable<Book>> GetAvailableBooksAsync();
    Task<Book?> GetBookWithCategoriesAsync(int id);
    Task<bool> IsBookAvailableAsync(int bookId, int quantity);
}
