using Microsoft.AspNetCore.Mvc;
using EFExample.Api.Data;
using EFExample.Api.Models;

[ApiController]
[Route("seed")]
public class SeedController : ControllerBase
{
    private readonly AppDbContext _db;

    public SeedController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Seed()
    {
        var dishes = new[]
        {
            new Dish { Name = "Tacos", Price = 10.99m },
            new Dish { Name = "Pizza", Price = 15.49m },
            new Dish { Name = "Burger", Price = 8.99m },
            new Dish { Name = "Sushi", Price = 20.00m }
        };

        _db.Dishes.AddRange(dishes);
        await _db.SaveChangesAsync();

        var orders = new[]
        {
            new Order { ClientName = "John Doe", CreatedAt = DateTime.UtcNow, DishId = 1 },
            new Order { ClientName = "Jane Smith", CreatedAt = DateTime.UtcNow, DishId = 2 },
            new Order { ClientName = "Bob Johnson", CreatedAt = DateTime.UtcNow, DishId = 3 },
            new Order { ClientName = "Alice Williams", CreatedAt = DateTime.UtcNow, DishId = 4 }
        };

        _db.Orders.AddRange(orders);
        await _db.SaveChangesAsync();

        return Ok("Database seeded with hardcoded dishes and orders.");
    }
}
