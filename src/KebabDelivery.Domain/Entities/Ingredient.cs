using FluentResults;

namespace KebabDelivery.Domain.Entities;

public class Ingredient
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Calories { get; private set; }
    public decimal Proteins { get; private set; }
    public decimal Fats { get; private set; }
    public decimal Carbohydrates { get; private set; }
    public bool IsAlcoholic { get; private set; }
    public bool ContainsLactose { get; private set; }
    public List<ProductIngredient> productIngredients { get; private set; } = new();

    private Ingredient() { }

    public static Result<Ingredient> Create(string name, decimal calories, decimal proteins, decimal fats, decimal carbs, bool isAlcoholic, bool containsLactose)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Fail("Название ингредиента не может быть пустым.");

        if (calories < 0)
            return Result.Fail("Калории не могут быть отрицательными.");

        if (proteins < 0)
            return Result.Fail("Белки не могут быть отрицательными.");

        if (fats < 0)
            return Result.Fail("Жиры не могут быть отрицательными.");

        if (carbs < 0)
            return Result.Fail("Углеводы не могут быть отрицательными.");

        return Result.Ok(new Ingredient
        {
            Id = Guid.NewGuid(),
            Name = name,
            Calories = calories,
            Proteins = proteins,
            Fats = fats,
            Carbohydrates = carbs,
            IsAlcoholic = isAlcoholic,
            ContainsLactose = containsLactose
        });
    }
}