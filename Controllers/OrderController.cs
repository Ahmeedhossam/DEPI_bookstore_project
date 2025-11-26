using Microsoft.AspNetCore.Mvc;
using Bookstore.Repositories.Interfaces;
using Bookstore.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Bookstore.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderController(ICartRepository cartRepository, IOrderRepository orderRepository)
    {
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    public async Task<IActionResult> Checkout()
    {
        var userId = GetUserId();
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);

        if (cart.Items.Count == 0)
        {
            TempData["ErrorMessage"] = "Your cart is empty.";
            return RedirectToAction("Index", "Cart");
        }

        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(string shippingAddress) // Simplified for now
    {
        var userId = GetUserId();
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);

        if (cart.Items.Count == 0)
        {
            return RedirectToAction("Index", "Cart");
        }

        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.Now,
            Status = OrderStatus.Pending,
            Items = cart.Items.Select(i => new OrderItem
            {
                BookId = i.BookId,
                Quantity = i.Quantity,
                Price = i.Book.Price // Snapshot price
            }).ToList()
        };

        await _orderRepository.CreateOrderAsync(order);
        await _cartRepository.ClearCartAsync(userId);

        return RedirectToAction("OrderConfirmation", new { id = order.Id });
    }

    public async Task<IActionResult> OrderConfirmation(int id)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);
        if (order == null || order.UserId != GetUserId())
        {
            return NotFound();
        }
        return View(order);
    }

    public async Task<IActionResult> Index()
    {
        var userId = GetUserId();
        var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
        return View(orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);
        if (order == null || order.UserId != GetUserId())
        {
            return NotFound();
        }
        return View(order);
    }
}
