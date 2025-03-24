using KebabDelivery.Domain.Base;
using KebabDelivery.Domain.Enums;
using KebabDelivery.Domain.Exceptions;

namespace KebabDelivery.Domain.Entities;

public class Ingredient : Consumable
{
    public bool IsComposite { get; private set; }
    public List<IngredientIngredient> SubIngredients { get; private set; } = new();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    protected Ingredient() : base(string.Empty, false, false, 0, 0, 0, 0)
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public Ingredient(string name, bool isComposite, decimal calories, decimal proteins, decimal fats, decimal carbohydrates, bool isAlcoholic, bool containsLactose)
        : base(name, isAlcoholic, containsLactose, calories, proteins, fats, carbohydrates)
    {
        IsComposite = isComposite;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void AddSubIngredient(Ingredient ingredient, decimal amount, MeasurementUnit unit)
    {
        if (!IsComposite)
            throw new DomainValidationException("A simple ingredient cannot contain sub-ingredients.");

        bool duplicateById = SubIngredients.Any(i => i.SubIngredientId == ingredient.Id);
        bool duplicateByName = SubIngredients.Any(i =>
            i.SubIngredient.Name.Equals(ingredient.Name, StringComparison.OrdinalIgnoreCase));

        if (duplicateById || duplicateByName)
            throw new DomainValidationException($"Ingredient '{ingredient.Name}' has already been added to the list.");

        SubIngredients.Add(new IngredientIngredient(this, ingredient, amount, unit));
        RecalculateNutrition();
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveSubIngredient(Guid ingredientId)
    {
        if (!IsComposite)
            throw new InvalidOperationException("A simple ingredient cannot contain sub-ingredients.");

        var subIngredient = SubIngredients.FirstOrDefault(i => i.SubIngredientId == ingredientId);
        if (subIngredient == null)
            throw new InvalidOperationException("The ingredient was not found in the composition.");

        SubIngredients.Remove(subIngredient);
        RecalculateNutrition();
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateSubIngredients(List<IngredientIngredient> newSubIngredients)
    {
        if (!IsComposite)
            throw new InvalidOperationException("A simple ingredient cannot contain sub-ingredients.");

        if (newSubIngredients == null || newSubIngredients.Count == 0)
            throw new InvalidOperationException("A compound ingredient must contain at least one sub ingredient.");

        SubIngredients.Clear();
        SubIngredients.AddRange(newSubIngredients);
        RecalculateNutrition();
        UpdatedAt = DateTime.UtcNow;
    }

    private void RecalculateNutrition()
    {
        if (!IsComposite) return;

        UpdateNutrition(
            SubIngredients.Sum(i => i.SubIngredient.Calories * (i.Amount / 100m)),
            SubIngredients.Sum(i => i.SubIngredient.Proteins * (i.Amount / 100m)),
            SubIngredients.Sum(i => i.SubIngredient.Fats * (i.Amount / 100m)),
            SubIngredients.Sum(i => i.SubIngredient.Carbohydrates * (i.Amount / 100m))
        );
    }
}