using KebabDelivery.Domain.Enums;
using KebabDelivery.Domain.Exceptions;
using KebabDelivery.Domain.Guards;

namespace KebabDelivery.Domain.Entities;

public class IngredientIngredient
{
    public Guid IngredientId { get; private set; }
    public Ingredient Ingredient { get; private set; } = null!;
    
    public Guid SubIngredientId { get; private set; }
    public Ingredient SubIngredient { get; private set; } = null!;
    
    public decimal Amount { get; private set; }
    public MeasurementUnit Unit { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected IngredientIngredient() { }

    public IngredientIngredient(Ingredient ingredient, Ingredient subIngredient, decimal amount, MeasurementUnit unit)
    {
        Guard.AgainstNull(ingredient, "Main ingredient cannot be null.");
        Guard.AgainstNull(subIngredient, "Sub ingredient cannot be null.");
        Guard.AgainstEqual(ingredient.Id, subIngredient.Id, "An ingredient cannot be under itself.");
        
        if (amount <= 0)
            throw new DomainValidationException("The number must be greater than 0.");

        Ingredient = ingredient;
        IngredientId = ingredient.Id;
        SubIngredient = subIngredient;
        SubIngredientId = subIngredient.Id;
        Amount = amount;
        Unit = unit;
        CreatedAt = DateTime.UtcNow;
    }
}