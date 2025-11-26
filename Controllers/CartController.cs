using Microsoft.AspNetCore.Mvc;
using Bookstore.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Bookstore.Entities;
using System.Security.Claims;

namespace Bookstore.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ICartRepository _cartRepository;
    private readonly IBookRepository _bookRepository;

    public CartController(ICartRepository cartRepository, IBookRepository bookRepository)
    {
        _cartRepository = cartRepository;
        _bookRepository = bookRepository;
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    public async Task<IActionResult> Index()
    {
        var userId = GetUserId();
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        return View(cart.Items.ToList());
    }

    public async Task<IActionResult> AddToCart(int bookId)
    {
        var userId = GetUserId();
        await _cartRepository.AddItemAsync(userId, bookId);
        TempData["SuccessMessage"] = "Book added to cart!";
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateQuantity(int bookId, int quantity)
    {
        var userId = GetUserId();
        await _cartRepository.UpdateQuantityAsync(userId, bookId, quantity);
        TempData["SuccessMessage"] = "Cart updated!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RemoveFromCart(int bookId)
    {
        var userId = GetUserId();
        await _cartRepository.RemoveItemAsync(userId, bookId);
        TempData["SuccessMessage"] = "Book removed from cart!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ClearCart()
    {
        var userId = GetUserId();
        await _cartRepository.ClearCartAsync(userId);
        TempData["SuccessMessage"] = "Cart cleared!";
        return RedirectToAction(nameof(Index));
    }
}
