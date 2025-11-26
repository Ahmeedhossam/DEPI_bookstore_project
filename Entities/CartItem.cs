using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Entities;

public class CartItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int CartId { get; set; }
    [ForeignKey("CartId")]
    public virtual Cart Cart { get; set; }

    public int BookId { get; set; }
    [ForeignKey("BookId")]
    public virtual Book Book { get; set; }

    public int Quantity { get; set; }

    [NotMapped]
    public decimal Total => Book?.Price * Quantity ?? 0;
}
