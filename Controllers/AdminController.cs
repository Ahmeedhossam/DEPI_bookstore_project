using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Repositories.Interfaces;
using Bookstore.Entities;

namespace Bookstore.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IBookRepository _bookRepository;
    private readonly ICategoryRepository _categoryRepository;

    public AdminController(IBookRepository bookRepository, ICategoryRepository categoryRepository)
    {
        _bookRepository = bookRepository;
        _categoryRepository = categoryRepository;
    }

    // GET: /Admin
    public async Task<IActionResult> Index()
    {
        var books = await _bookRepository.GetAllAsync();
        return View(books);
    }

    // GET: /Admin/Add
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        ViewBag.Categories = await _categoryRepository.GetAllAsync();
        return View();
    }

    // POST: /Admin/Add
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(Book book, int[] selectedCategoryIds)
    {
        // Remove Id from ModelState as it's auto-generated
        ModelState.Remove("Id");
        ModelState.Remove("OrderItems");
        ModelState.Remove("BookCategories");

        if (ModelState.IsValid)
        {
            if (selectedCategoryIds != null)
            {
                foreach (var categoryId in selectedCategoryIds)
                {
                    book.BookCategories.Add(new BookCategory { CategoryId = categoryId });
                }
            }

            await _bookRepository.AddAsync(book);
            TempData["SuccessMessage"] = "Book added successfully!";
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = await _categoryRepository.GetAllAsync();
        return View(book);
    }

    // GET: /Admin/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookRepository.GetBookWithCategoriesAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        ViewBag.Categories = await _categoryRepository.GetAllAsync();
        return View(book);
    }

    // POST: /Admin/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Book book, int[] selectedCategoryIds)
    {
        if (id != book.Id)
        {
            return NotFound();
        }

        ModelState.Remove("OrderItems");
        ModelState.Remove("BookCategories");

        if (ModelState.IsValid)
        {
            try
            {
                // We need to fetch the existing book with categories to update them
                var existingBook = await _bookRepository.GetBookWithCategoriesAsync(id);
                if (existingBook == null) return NotFound();

                // Update properties
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Price = book.Price;
                existingBook.Description = book.Description;
                existingBook.PublishedDate = book.PublishedDate;
                existingBook.CopiesAvailable = book.CopiesAvailable;
                existingBook.CoverImageUrl = book.CoverImageUrl;

                // Update Categories
                existingBook.BookCategories.Clear();
                if (selectedCategoryIds != null)
                {
                    foreach (var categoryId in selectedCategoryIds)
                    {
                        existingBook.BookCategories.Add(new BookCategory { BookId = id, CategoryId = categoryId });
                    }
                }

                await _bookRepository.UpdateAsync(existingBook);
                TempData["SuccessMessage"] = "Book updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                if (!await BookExists(book.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        ViewBag.Categories = await _categoryRepository.GetAllAsync();
        return View(book);
    }

    // POST: /Admin/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book != null)
        {
            await _bookRepository.DeleteAsync(book);
            TempData["SuccessMessage"] = "Book deleted successfully!";
        }
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> BookExists(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        return book != null;
    }

    // GET: /Admin/Orders
    public async Task<IActionResult> Orders()
    {
        // We need to inject IOrderRepository. 
        // Since we can't easily change the constructor in this replace block without replacing the whole file, 
        // I will use RequestServices to get the repository. 
        // Ideally, we should update the constructor.
        var orderRepository = HttpContext.RequestServices.GetService<IOrderRepository>();
        if (orderRepository == null) return Problem("OrderRepository not found");

        var orders = await orderRepository.GetAllOrdersAsync();
        return View(orders);
    }

    // POST: /Admin/UpdateOrderStatus
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderStatus status)
    {
        var orderRepository = HttpContext.RequestServices.GetService<IOrderRepository>();
        if (orderRepository == null) return Problem("OrderRepository not found");

        await orderRepository.UpdateOrderStatusAsync(orderId, status);
        TempData["SuccessMessage"] = "Order status updated successfully!";
        return RedirectToAction(nameof(Orders));
    }
}
