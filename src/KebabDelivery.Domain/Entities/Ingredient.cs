using FluentResults;

namespace KebabDelivery.Domain.Entities;

public class Ingredient
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public List<ProductIngredient> ProductIngredients { get; private set; } = new();

    private Ingredient() { }

    public static Result<Ingredient> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Fail("Название ингредиента не может быть пустым.");

        return Result.Ok(new Ingredient { Id = Guid.NewGuid(), Name = name });
    }
}