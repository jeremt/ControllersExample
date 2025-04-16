using System;

namespace EFExample.Api.Models;

public class Order
{
    public int OrderId { get; set; }
    public required string ClientName { get; set; }
    public required DateTime CreatedAt { get; set; }

    public int DishId { get; set; }
    public Dish Dish { get; set; } = null!; // ✅ tell compiler it will be set by EF
}

public class OrderDto
{
    public required int OrderId { get; set; }
    public required string ClientName { get; set; }
    public required DateTime CreatedAt { get; set; }

    public required DishDto Dish { get; set; }

    public class DishDto
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
    }
}

public class CreateOrderDto
{
    public required string ClientName { get; set; }
    public required int DishId { get; set; }
    public DateTime? CreatedAt { get; set; } // Optional; defaults to now
}

public class UpdateDishDto
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}