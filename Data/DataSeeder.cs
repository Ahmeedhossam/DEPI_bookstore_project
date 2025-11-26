using Bookstore.Entities;
using Microsoft.AspNetCore.Identity;
using Bookstore.Context;

namespace Bookstore.Data;

public class DataSeeder
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        // Seed Roles
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
        string[] roleNames = { "Admin", "User" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole<int>(roleName));
            }
        }

        // Seed Admin User
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var adminEmail = "admin@depi.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Admin",
                LastName = "User",
                PhoneNumber = "1234567890",
                Address = "Admin HQ",
                JoiningDate = DateTime.Now,
                IsAdmin = true,
                EmailConfirmed = true
            };
            var createPowerUser = await userManager.CreateAsync(adminUser, "Admin123!");
            if (createPowerUser.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }

    public static async Task SeedBooksAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<BookstoreContext>();

        if (!context.Books.Any())
        {
            context.Books.AddRange(
                new Book
                {
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    Price = 12.99m,
                    Description = "A novel set in the Jazz Age that explores themes of wealth and excess.",
                    PublishedDate = new DateTime(1925, 4, 10),
                    CopiesAvailable = 10,
                    CoverImageUrl = "https://upload.wikimedia.org/wikipedia/commons/7/7a/The_Great_Gatsby_Cover_1925_Retouched.jpg"
                },
                new Book
                {
                    Title = "1984",
                    Author = "George Orwell",
                    Price = 9.99m,
                    Description = "A dystopian social science fiction novel and cautionary tale.",
                    PublishedDate = new DateTime(1949, 6, 8),
                    CopiesAvailable = 15,
                    CoverImageUrl = "https://upload.wikimedia.org/wikipedia/en/c/c3/1984_George_Orwell.jpg"
                },
                new Book
                {
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    Price = 14.50m,
                    Description = "A novel about the serious issues of rape and racial inequality.",
                    PublishedDate = new DateTime(1960, 7, 11),
                    CopiesAvailable = 8,
                    CoverImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4f/To_Kill_a_Mockingbird_%28first_edition_cover%29.jpg"
                },
                new Book
                {
                    Title = "Pride and Prejudice",
                    Author = "Jane Austen",
                    Price = 11.25m,
                    Description = "A romantic novel of manners.",
                    PublishedDate = new DateTime(1813, 1, 28),
                    CopiesAvailable = 20,
                    CoverImageUrl = "https://upload.wikimedia.org/wikipedia/commons/1/13/Pride_and_Prejudice_1813_title_page.jpg"
                },
                new Book
                {
                    Title = "The Catcher in the Rye",
                    Author = "J.D. Salinger",
                    Price = 10.00m,
                    Description = "A story about adolescent alienation and loss of innocence.",
                    PublishedDate = new DateTime(1951, 7, 16),
                    CopiesAvailable = 12,
                    CoverImageUrl = "https://upload.wikimedia.org/wikipedia/commons/8/89/The_Catcher_in_the_Rye_%281951%2C_first_edition_cover%29.jpg"
                },
                new Book
                {
                    Title = "The Hobbit",
                    Author = "J.R.R. Tolkien",
                    Price = 15.99m,
                    Description = "A fantasy novel and children's book.",
                    PublishedDate = new DateTime(1937, 9, 21),
                    CopiesAvailable = 25,
                    CoverImageUrl = "https://upload.wikimedia.org/wikipedia/en/4/4a/TheHobbit_FirstEdition.jpg"
                }
            );
            await context.SaveChangesAsync();
        }
    }
}
