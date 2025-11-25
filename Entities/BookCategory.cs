using System.ComponentModel.DataAnnotations;

namespace Bookstore.Entities;

public class BookCategory
{
    [Key]
    public int BookId { get; set; }
    
    [Key]
    public int CategoryId { get; set; }

    public Book Book { get; set; }
    public Category Category { get; set; }
}