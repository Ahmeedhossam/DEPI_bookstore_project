namespace Bookstore.Controllers;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Context;
using Bookstore.Entities;
using Microsoft.EntityFrameworkCore;

public class BookController : Controller{
    private readonly BookstoreContext _context;

    public BookController(BookstoreContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _context.Books.ToListAsync();
        return View(books);
    }
}