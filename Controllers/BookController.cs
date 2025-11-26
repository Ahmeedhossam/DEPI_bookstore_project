using Bookstore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers;

public class BookController : Controller
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    // GET: /Book
    public async Task<IActionResult> Index(string? search)
    {
        var books = await _bookRepository.GetAllAsync();
        return View(books);
    }

    public async Task<IActionResult> Details(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }
}