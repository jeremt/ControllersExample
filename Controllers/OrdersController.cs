using Microsoft.AspNetCore.Mvc;
using EFExample.Api.Data;
using EFExample.Api.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _db;

    public OrdersController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetAll([FromQuery] string? dishName)
    {
        var query = _db.Orders.Include(o => o.Dish).AsQueryable();

        if (!string.IsNullOrWhiteSpace(dishName))
            query = query.Where(o => o.Dish.Name.Contains(dishName));

        var results = await query
            .Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                ClientName = o.ClientName,
                CreatedAt = o.CreatedAt,
                Dish = new OrderDto.DishDto
                {
                    Name = o.Dish.Name,
                    Price = o.Dish.Price
                }
            }).ToListAsync();

        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> Get(int id)
    {
        var order = await _db.Orders.Include(o => o.Dish)
                                    .FirstOrDefaultAsync(o => o.OrderId == id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> Create(CreateOrderDto input)
    {
        var dish = await _db.Dishes.FindAsync(input.DishId);
        if (dish == null)
            return BadRequest($"Dish with id {input.DishId} not found.");

        var order = new Order
        {
            ClientName = input.ClientName,
            DishId = input.DishId,
            CreatedAt = input.CreatedAt ?? DateTime.UtcNow
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = order.OrderId }, order);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _db.Orders.FindAsync(id);
        if (order == null) return NotFound();

        _db.Orders.Remove(order);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
