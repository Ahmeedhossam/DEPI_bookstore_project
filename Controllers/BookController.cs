using Microsoft.AspNetCore.Mvc;
using Bookstore.Context;
using Bookstore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Controllers;

public class BookController : Controller
{
    private readonly BookstoreContext _context;

    public BookController(BookstoreContext context)
    {
        _context = context;
    }

    // GET: /Book
    public async Task<IActionResult> Index(string? search)
    {
        var query = _context.Books.AsQueryable();
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(b => b.Title.Contains(search) || b.Author.Contains(search));
        }
        var books = await query
            .OrderBy(b => b.Title)
            .ToListAsync();
        return View(books);
    }

    // GET: /Book/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book == null) return NotFound();
        return View(book);
    }

    // GET: /Book/Create
    public IActionResult Create()
    {
        return View(new Book { PublishedDate = DateTime.Today });
    }

    // POST: /Book/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book model)
    {
        if (!ModelState.IsValid) return View(model);

        _context.Books.Add(model);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Book added successfully.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Book/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();
        return View(book);
    }

    // POST: /Book/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Book model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        _context.Update(model);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Book updated.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Book/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();
        return View(book);
    }

    // POST: /Book/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Book deleted.";
        return RedirectToAction(nameof(Index));
    }
}