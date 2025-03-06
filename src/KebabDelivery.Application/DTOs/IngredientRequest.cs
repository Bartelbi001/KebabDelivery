namespace KebabDelivery.Application.DTOs;

public class IngredientRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Calories { get; set; }
    public decimal Proteins { get; set; }
    public decimal Fats { get; set; }
    public decimal Carbohydrates { get; set; }
    public bool? IsAlcoholic { get; set; } // NULL = Не применимо
    public bool? ContainsLactose { get; set; } // NULL = Не применимо

    public IngredientRequest() { }

    public IngredientRequest(string name, decimal calories, decimal proteins, decimal fats, decimal carbs, bool isAlcoholic, bool containsLactose)
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