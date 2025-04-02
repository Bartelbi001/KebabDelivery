using KebabDelivery.Domain.Guards;
using KebabDelivery.Domain.ValueObjects;

namespace KebabDelivery.Domain.Entities;

public class IngredientIngredient
{
    public Guid IngredientId { get; private set; }
    public Ingredient Ingredient { get; private set; } = null!;
    
    public Guid SubIngredientId { get; private set; }
    public Ingredient SubIngredient { get; private set; } = null!;
    
    
    public Measurement Measurement { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }

    private IngredientIngredient() { }

    public IngredientIngredient(Ingredient ingredient, Ingredient subIngredient, Measurement measurement)
    {
        Guard.AgainstNull(ingredient, "Ingredient is required.");
        Guard.AgainstNull(subIngredient, "SubIngredient is required.");
        Guard.AgainstNull(measurement, "Measurement is required.");
        Guard.AgainstEqual(ingredient.Id, subIngredient.Id, "An ingredient cannot contain itself.");
        
        Ingredient = ingredient;
        IngredientId = ingredient.Id;
        SubIngredientId = subIngredient.Id;
        SubIngredientId = subIngredient.Id;
        Measurement = measurement;
        CreatedAt = DateTime.Now;
    }
}