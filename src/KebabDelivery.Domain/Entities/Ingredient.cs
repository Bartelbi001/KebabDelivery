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
    public List<ProductIngredient> ProductIngredients { get; private set; } = new();

    private Ingredient() { }

    private Ingredient(Guid id, string name, decimal calories, decimal proteins, decimal fats, decimal carbs, bool isAlcoholic, bool containsLactose)
    {
        Id = id;
        Name = name;
        Calories = calories;
        Proteins = proteins;
        Fats = fats;
        Carbohydrates = carbs;
        IsAlcoholic = isAlcoholic;
        ContainsLactose = containsLactose;
    }

    public static Ingredient Create(string name, decimal calories, decimal proteins, decimal fats, decimal carbs, bool isAlcoholic, bool containsLactose)
    {
        return new Ingredient(Guid.NewGuid(), name, calories, proteins, fats, carbs, isAlcoholic, containsLactose);
    }

    public void Update(string name, decimal calories, decimal proteins, decimal fats, decimal carbs, bool isAlcoholic, bool containsLactose)
    {
        Name = name;
        Calories = calories;
        Proteins = proteins;
        Fats = fats;
        Carbohydrates = carbs;
        IsAlcoholic = isAlcoholic;
        ContainsLactose = containsLactose;
    }
}