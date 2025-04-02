using KebabDelivery.Domain.Base;
using KebabDelivery.Domain.Exceptions;
using KebabDelivery.Domain.Guards;
using KebabDelivery.Domain.Services;
using KebabDelivery.Domain.ValueObjects;

namespace KebabDelivery.Domain.Entities;

public class Ingredient : Consumable
{
    public bool IsComposite { get; private set; }
    public List<IngredientIngredient> SubIngredients { get; private set; } = new();

    private Ingredient() { }

    public Ingredient(string name, bool isComposite, Nutrition nutrition, bool isAlcoholic, bool containsLactose)
        : base(name, isAlcoholic, containsLactose, nutrition)
    {
        IsComposite = isComposite;
    }

    public void AddSubIngredient(IngredientIngredient subIngredient)
    {
        Guard.AgainstNull(subIngredient, "SubIngredient is required.");
        
        if (!IsComposite)
            throw new DomainValidationException("Cannot add sub-ingredients to a non-composite ingredient.");

        if (SubIngredients.Any(i => i.SubIngredient.Id == subIngredient.SubIngredient.Id))
            throw new DomainValidationException("Sub-ingredient already exists.");

        SubIngredients.Add(subIngredient);
        UpdateNutrition(RecipeNutritionalAnalyzer.Calculate(SubIngredients));
        SetUpdatedNow();
    }

    public void RemoveSubIngredient(Guid subIngredientId)
    {
        var sub = SubIngredients.FirstOrDefault(i => i.SubIngredient.Id == subIngredientId);
        if (sub != null)
            throw new DomainValidationException("Sub-ingredient not found.");
            
        SubIngredients.Remove(sub);
        UpdateNutrition(RecipeNutritionalAnalyzer.Calculate(SubIngredients));
        SetUpdatedNow();
    }

    public void UpdateSubIngredients(List<IngredientIngredient> subIngredients)
    {
        Guard.AgainstNull(subIngredients, "SubIngredients are required.");
        
        SubIngredients = subIngredients;
        UpdateNutrition(RecipeNutritionalAnalyzer.Calculate(SubIngredients));
        SetUpdatedNow();
    }
}