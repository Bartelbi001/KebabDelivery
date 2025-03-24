using FluentResults;

namespace KebabDelivery.Domain.Entities;

public class ProductSize
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }

    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Amount { get; private set; }

    protected ProductSize() { }

    public static Result<ProductSize> Create(Guid productId, string name, decimal price, int amount)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Fail("The size name cannot be empty.");

        if (price <= 0)
            return Result.Fail("The price should be positive.");

        if (amount <= 0)
            return Result.Fail("The quantity must be greater than zero.");

        return Result.Ok(new ProductSize
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Name = name,
            Price = price,
            Amount = amount
        });
    }
}