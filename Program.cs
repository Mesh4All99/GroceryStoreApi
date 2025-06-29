using GroceryStore.Data;
using GroceryStore.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GroceryStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//add Authentication to DI Container and add to it the JwtBearerToken.
builder.Services.AddAuthentication().AddBearerToken();

// add Authorization to DI Container.

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", Policy =>
    {
        Policy.RequireRole("Admin");
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

// getting service scope
using (var scope = app.Services.CreateScope())
{
    // Ensuring Database is okay
    var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    _context.Database.EnsureCreated();

    if (_context.Database.GetPendingMigrations().Any())
    {
        _context.Database.Migrate();
    }

    // Seeding Dummy Data
    await _context.Items.AddRangeAsync(
        new Item
        {
            Name = "Milk",
            ComanyName = "Al-Hana",
            DateOfPurchase = DateTime.Now,
            Price = 35,
            SupplierName = null
        },
        new Item
        {
            Name = "Battery",
            ComanyName = "Doracell",
            DateOfPurchase = DateTime.Now,
            Price = 10,
            SupplierName = "Doracell Supplier"
        },
        new Item
        {
            Name = "Tea",
            ComanyName = "Al-Kabous",
            DateOfPurchase = DateTime.Parse("20-06-2025"),
            Price = 25,
            SupplierName = "Al-Kabous"
        },
        new Item
        {
            Name = "Chiken",
            ComanyName = "Helal",
            DateOfPurchase = DateTime.Parse("25-06-2025"),
            Price = 25,
            SupplierName = "Helal poultry"
        },
        new Item
        {
            Name = "Ice",
            ComanyName = null,
            DateOfPurchase = DateTime.Now,
            Price = 1,
            SupplierName = null
        }
        );
    await _context.SaveChangesAsync();

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<IdentityUser>();
app.MapControllers();

app.Run();
