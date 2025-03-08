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

    private IngredientIngredient() { }

    public IngredientIngredient(Ingredient ingredient, Ingredient subIngredient, decimal amount, MeasurementUnit unit)
    {
        if (ingredient == null)
            throw new DomainValidationException("Главный ингредиент не может быть null.");

        if (subIngredient == null)
            throw new DomainValidationException("Подингредиент не может быть null.");

        Ingredient = ingredient;
        IngredientId = ingredient.Id;
        SubIngredient = subIngredient;
        SubIngredientId = subIngredient.Id;
        Amount = amount;
        Unit = unit;
        CreatedAt = DateTime.UtcNow;
    }
}