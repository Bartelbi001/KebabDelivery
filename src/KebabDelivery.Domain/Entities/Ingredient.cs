using KebabDelivery.Domain.Base;

namespace KebabDelivery.Domain.Entities;

public class Ingredient : Consumable
{
    public bool IsComposite { get; private set; }
    public List<IngredientIngredient> SubIngredients { get; private set; } = new();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Ingredient() : base(string.Empty, false, false, 0, 0, 0, 0)
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


}