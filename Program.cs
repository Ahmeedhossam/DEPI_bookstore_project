using Microsoft.EntityFrameworkCore;
using Bookstore.Context;
using Bookstore.Entities;
using Microsoft.AspNetCore.Identity;
using Bookstore.Services.Interfaces;
using Bookstore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // Add Session
builder.Services.AddHttpContextAccessor(); // Add HttpContextAccessor
builder.Services.AddDbContext<BookstoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity with integer keys and EF Core stores
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<BookstoreContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<BookstoreContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// Register Services
builder.Services.AddScoped<IAuthService, AuthService>();

// Register Repositories
builder.Services.AddScoped<Bookstore.Repositories.Interfaces.IBookRepository, Bookstore.Repositories.BookRepository>();
builder.Services.AddScoped<Bookstore.Repositories.Interfaces.ICategoryRepository, Bookstore.Repositories.CategoryRepository>();
builder.Services.AddScoped<Bookstore.Repositories.Interfaces.IOrderRepository, Bookstore.Repositories.OrderRepository>();
builder.Services.AddScoped<Bookstore.Repositories.Interfaces.IOrderItemRepository, Bookstore.Repositories.OrderItemRepository>();
builder.Services.AddScoped<Bookstore.Repositories.Interfaces.IOrderItemRepository, Bookstore.Repositories.OrderItemRepository>();
builder.Services.AddScoped<Bookstore.Repositories.Interfaces.IUserRepository, Bookstore.Repositories.UserRepository>();
builder.Services.AddScoped<Bookstore.Repositories.Interfaces.ICartRepository, Bookstore.Repositories.CartRepository>();

var app = builder.Build();

// Seed Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await Bookstore.Data.DataSeeder.SeedRolesAndAdminAsync(services);
    await Bookstore.Data.DataSeeder.SeedBooksAsync(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // Use Session

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
