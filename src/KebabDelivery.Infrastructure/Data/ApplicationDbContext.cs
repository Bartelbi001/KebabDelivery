using KebabDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KebabDelivery.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<ProductIngredient> ProductIngredients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}