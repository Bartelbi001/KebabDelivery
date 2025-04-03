using KebabDelivery.Domain.Base;
using KebabDelivery.Domain.Exceptions;
using KebabDelivery.Domain.Guards;
using KebabDelivery.Domain.ValueObjects;

namespace KebabDelivery.Domain.Entities;

public class Product : Consumable
{
    public Price Price { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string? ImageUrl { get; private set; }
    public bool IsAvailable { get; private set; }
    public bool IsDeleted { get; private set; }

    public List<ProductSize> Sizes { get; private set; } = new();
    public List<ProductIngredient> Ingredients { get; private set; } = new();
    
    private Product() { }

    public Product(string name, Nutrition nutrition, Price price, bool isAlcoholic, bool containsLactose,
        string? description = null, string? imageUrl = null)
        : base(name, isAlcoholic, containsLactose, nutrition)
    {
        Guard.AgainstNull(price, "Price is required.");
        
        Price = price;
        Description = description?.Trim() ?? string.Empty;
        ImageUrl = imageUrl?.Trim();
        IsAvailable = true;
        IsDeleted = false;
    }
}