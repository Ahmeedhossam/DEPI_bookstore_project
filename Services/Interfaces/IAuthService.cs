using Bookstore.Entities;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Services.Interfaces;

public interface IAuthService
{
    // Registration
    Task<IdentityResult> RegisterAsync(string email, string password, string firstName, string lastName, string phoneNumber, string? address = null);
    
    // Login/Logout
    Task<SignInResult> LoginAsync(string email, string password, bool rememberMe);
    Task LogoutAsync();
    
    Task<User?> FindByEmailAsync(string email);
    Task<User?> FindByIdAsync(int userId);
    
    // Password Management
    Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
    Task<string> GeneratePasswordResetTokenAsync(User user);
    Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
    
    // Email Confirmation
    Task<string> GenerateEmailConfirmationTokenAsync(User user);
    Task<IdentityResult> ConfirmEmailAsync(User user, string token);
    
    // Validation
    Task<bool> IsEmailUniqueAsync(string email);

    Task<bool> CheckPasswordAsync(User user, string password);

    // User Management
    Task<User?> GetCurrentUserAsync(System.Security.Claims.ClaimsPrincipal user);
    Task<IdentityResult> UpdateUserAsync(User user);
}
