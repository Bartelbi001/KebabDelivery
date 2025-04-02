using KebabDelivery.Domain.Entities;
using KebabDelivery.Domain.ValueObjects;

namespace KebabDelivery.Domain.Services;

public static class RecipeNutritionalAnalyzer
{
    public static Nutrition Calculate(IEnumerable<IngredientIngredient> subIngredients)
    {
        var calories = subIngredients.Sum(i => i.SubIngredient.Nutrition.Calories * (i.Measurement.Amount / 100));
        var proteins = subIngredients.Sum(i => i.SubIngredient.Nutrition.Proteins * (i.Measurement.Amount / 100));
        var fats = subIngredients.Sum(i => i.SubIngredient.Nutrition.Fats * (i.Measurement.Amount / 100));
        var carbohydrates = subIngredients.Sum(i => i.SubIngredient.Nutrition.Carbohydrates * (i.Measurement.Amount / 100));

        return new Nutrition(calories, proteins, fats, carbohydrates);
    }
}