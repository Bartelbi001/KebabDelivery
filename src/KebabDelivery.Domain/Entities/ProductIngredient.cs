using FluentResults;
using KebabDelivery.Domain.Guards;
using KebabDelivery.Domain.ValueObjects;

namespace KebabDelivery.Domain.Entities;

public class ProductIngredient
{
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = null!;

    public Guid IngredientId { get; private set; }
    public Ingredient Ingredient { get; private set; } = null!;

    public Measurement Measurement { get; private set; }
    public DateTime CreatedDate { get; private set; }

    private ProductIngredient() { }

    public ProductIngredient(Product product, Ingredient ingredient, Measurement measurement)
    {
        Guard.AgainstNull(product, "Product is required.");
        Guard.AgainstNull(ingredient, "Ingredient is required.");
        Guard.AgainstEqual(product.Id, ingredient.Id, "Product cannot contain itself.");
        Guard.AgainstNull(Measurement, "Measurement is required.");
        
        Product = product;
        ProductId = product.Id;
        Ingredient = ingredient;
        IngredientId = ingredient.Id;
        Measurement = measurement;
        CreatedDate = DateTime.UtcNow;
    }
}