using Microsoft.AspNetCore.Mvc;
using EFExample.Api.Data;
using EFExample.Api.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("dishes")]
public class DishesController : ControllerBase
{
    private readonly AppDbContext _db;

    public DishesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Dish>>> GetAll()
    {
        var dishes = await _db.Dishes.ToListAsync();
        return Ok(dishes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Dish>> Get(int id)
    {
        var dish = await _db.Dishes.FindAsync(id);
        if (dish == null) return NotFound();
        return Ok(dish);
    }

    [HttpPost]
    public async Task<ActionResult<Dish>> Create(CreateDishDto dto)
    {
        var dish = new Dish { Name = dto.Name, Price = dto.Price };
        _db.Dishes.Add(dish);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = dish.DishId }, dish);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDishDto dto)
    {
        var dish = await _db.Dishes.FindAsync(id);
        if (dish == null) return NotFound();

        dish.Name = dto.Name;
        dish.Price = dto.Price;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var dish = await _db.Dishes.FindAsync(id);
        if (dish == null) return NotFound();

        _db.Dishes.Remove(dish);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
