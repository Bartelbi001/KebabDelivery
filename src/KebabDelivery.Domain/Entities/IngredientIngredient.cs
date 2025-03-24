using KebabDelivery.Domain.Enums;
using KebabDelivery.Domain.Exceptions;

namespace KebabDelivery.Domain.Entities;

public class IngredientIngredient
{
    public Guid IngredientId { get; private set; }
    public Ingredient Ingredient { get; private set; }
    public Guid SubIngredientId { get; private set; }
    public Ingredient SubIngredient { get; private set; }
    public decimal Amount { get; private set; }
    public MeasurementUnit Unit { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected IngredientIngredient() { }

    public IngredientIngredient(Ingredient ingredient, Ingredient subIngredient, decimal amount, MeasurementUnit unit)
    {
        if (ingredient == null)
            throw new DomainValidationException("The main ingredient cannot be null.");

        if (subIngredient == null)
            throw new DomainValidationException("The ingredient must not be null.");
        
        if (ingredient.Id == subIngredient.Id)
            throw new DomainValidationException("An ingredient cannot be under an ingredient by itself.");
        
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