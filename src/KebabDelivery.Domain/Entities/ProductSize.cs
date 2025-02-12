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

    private ProductSize() { }

    public static Result<ProductSize> Create(Guid productId, string name, decimal price, int amount)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Fail("Название размера не может быть пустым.");

        if (price <= 0)
            return Result.Fail("Цена должна быть положительной.");

        if (amount <= 0)
            return Result.Fail("Количество должно быть больше нуля.");

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