using KebabDelivery.Domain.Entities;
using KebabDelivery.Domain.ValueObjects;

namespace KebabDelivery.Domain.Services;

public static class ProductNutritionCalculator
{
    public static Nutrition Calculate(IEnumerable<ProductIngredient> ingredients)
    {
        var calories = ingredients.Sum(i => i.Ingredient.Nutrition.Calories * (i.Measurement.Amount / 100));
        var proteins = ingredients.Sum(i => i.Ingredient.Nutrition.Proteins * (i.Measurement.Amount / 100));
        var fats = ingredients.Sum(i => i.Ingredient.Nutrition.Fats * (i.Measurement.Amount / 100));
        var carbohydrates = ingredients.Sum(i => i.Ingredient.Nutrition.Carbohydrates * (i.Measurement.Amount / 100));

        return new Nutrition(calories, proteins, fats, carbohydrates);
    }
}