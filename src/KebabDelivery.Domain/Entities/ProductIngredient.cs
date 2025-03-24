using FluentResults;

namespace KebabDelivery.Domain.Entities;

public class ProductIngredient
{
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }

    public Guid IngredientId { get; private set; }
    public Ingredient Ingredient { get; private set; }

    public decimal Quantity { get; private set; }

    protected ProductIngredient() { }

    public static Result<ProductIngredient> Create(Guid productId, Guid ingredientId, decimal quantity)
    {
        if (quantity <= 0)
            return Result.Fail("Количество ингредиента должно быть положительным.");

        return Result.Ok(new ProductIngredient
        {
            ProductId = productId,
            IngredientId = ingredientId,
            Quantity = quantity
        });
    }
}