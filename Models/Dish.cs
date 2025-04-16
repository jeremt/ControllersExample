using System.Text.Json.Serialization;

namespace EFExample.Api.Models;

public class Dish
{
    public int DishId { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }

    [JsonIgnore]
    public List<Order> Orders { get; set; } = new();
}

public class CreateDishDto
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}