namespace Bookstore.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public DateTime OrderDate { get; set; } = DateTime.Now;

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    // Each order has multiple OrderItem lines (book + quantity)
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    [ForeignKey("UserId")]
    public User User { get; set; }

    [NotMapped]
    public decimal TotalPrice
    {
        get
        {
            decimal total = 0;
            foreach (var item in Items)
                total += item.Quantity * item.Book.Price;
            
            return total;
        }
    }
}