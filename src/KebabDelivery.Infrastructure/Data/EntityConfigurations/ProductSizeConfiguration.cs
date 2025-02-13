using KebabDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KebabDelivery.Infrastructure.Data.EntityConfigurations;

class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
{
    public void Configure(EntityTypeBuilder<ProductSize> builder)
    {
        builder.HasKey(ps => ps.Id);

        builder.Property(ps => ps.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ps => ps.Price)
            .HasPrecision(10, 2);
    }
}
