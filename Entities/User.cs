namespace Bookstore.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
    [Required, MaxLength(50)]
    public string FirstName { get; set; }

    [Required, MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    public DateTime JoiningDate { get; set; } = DateTime.Now;

    [Required]
    public bool IsAdmin { get; set; } = false;

    public bool IsActive { get; set; } = true;

    // Navigation to orders placed by the user
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
}