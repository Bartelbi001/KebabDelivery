using KebabDelivery.Application.DTOs;
using KebabDelivery.Domain.Entities;

namespace KebabDelivery.Application.Mapping;

public static class IngredientMapping
{
    public static IngredientResponse ToResponse(this Ingredient ingredient)
    {
        return new IngredientResponse(
            ingredient.Id,
            ingredient.Name,
            ingredient.Calories,
            ingredient.Proteins,
            ingredient.Fats,
            ingredient.Carbohydrates,
            ingredient.IsAlcoholic,
            ingredient.ContainsLactose);
    }

    public static List<IngredientResponse> ToResponseList(this IEnumerable<Ingredient> ingredients)
    {
        return ingredients.Select(i => i.ToResponse()).ToList();
    }
}