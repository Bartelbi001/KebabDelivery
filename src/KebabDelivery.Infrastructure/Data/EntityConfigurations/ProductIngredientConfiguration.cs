using KebabDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KebabDelivery.Infrastructure.Data.EntityConfigurations;

class ProductIngredientConfiguration : IEntityTypeConfiguration<ProductIngredient>
{
    public void Configure(EntityTypeBuilder<ProductIngredient> builder)
    {
        builder.HasKey(pi => new { pi.ProductId, pi.IngredientId });

        builder.HasOne(pi => pi.Product)
            .WithMany(p => p.ProductIngredients)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pi => pi.Ingredient)
            .WithMany(i => i.productIngredients)
            .HasForeignKey(pi => pi.IngredientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}