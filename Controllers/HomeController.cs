using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DEPI_bookstore.Models;
using Bookstore.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DEPI_bookstore.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBookRepository _bookRepository;
    private readonly ICategoryRepository _categoryRepository;

    public HomeController(ILogger<HomeController> logger, IBookRepository bookRepository, ICategoryRepository categoryRepository)
    {
        _logger = logger;
        _bookRepository = bookRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IActionResult> Index(string searchString, int? categoryId)
    {
        var categories = await _categoryRepository.GetAllAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name", categoryId);

        IEnumerable<Bookstore.Entities.Book> books;

        if (categoryId.HasValue)
        {
            books = await _bookRepository.GetBooksByCategoryAsync(categoryId.Value);
            // Ensure we only show available books
            books = books.Where(b => b.CopiesAvailable > 0);
        }
        else
        {
            books = await _bookRepository.GetAvailableBooksAsync();
        }

        if (!string.IsNullOrEmpty(searchString))
        {
            books = books.Where(b => b.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) || 
                                     b.Author.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }

        return View(books);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
