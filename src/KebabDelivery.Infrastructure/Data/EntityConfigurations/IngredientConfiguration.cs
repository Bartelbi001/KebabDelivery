using KebabDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KebabDelivery.Infrastructure.Data.EntityConfigurations;

class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasKey(i => i.Id);

        builder.HasIndex(i => i.Name)
            .IsUnique();

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.Calories)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(i => i.Proteins)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(i => i.Fats)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(i => i.Carbohydrates)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(i => i.IsAlcoholic)
            .HasDefaultValue(false);

        builder.Property(i => i.ContainsLactose)
            .HasDefaultValue(false);
    }
}