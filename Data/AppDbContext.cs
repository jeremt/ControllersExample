using Microsoft.EntityFrameworkCore;
using EFExample.Api.Models;

namespace EFExample.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Dish> Dishes => Set<Dish>();
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Dish)
            .WithMany(d => d.Orders)
            .HasForeignKey(o => o.DishId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
