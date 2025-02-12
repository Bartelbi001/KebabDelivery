using FluentResults;

namespace KebabDelivery.Domain.Entities;

public class ProductIngredient
{
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }

    public Guid IngredientId { get; private set; }
    public Ingredient Ingredient { get; private set; }

    private ProductIngredient() { }

    public static Result<ProductIngredient> Create(Guid productId, Guid ingredientId)
    {
        return Result.Ok(new ProductIngredient { ProductId = productId, IngredientId = ingredientId });
    }
}