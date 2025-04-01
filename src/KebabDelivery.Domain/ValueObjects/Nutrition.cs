using KebabDelivery.Domain.Common;
using KebabDelivery.Domain.Guards;

namespace KebabDelivery.Domain.ValueObjects;

public class Nutrition : ValueObject
{
    public decimal Calories { get; }
    public decimal Proteins { get; }
    public decimal Fats { get; }
    public decimal Carbohydrates { get; }

    public Nutrition(decimal calories, decimal proteins, decimal fats, decimal carbohydrates)
    {
        Guard.AgainstNegative(calories, "Calories cannot be negative.");
        Guard.AgainstNegative(proteins, "Proteins cannot be negative.");
        Guard.AgainstNegative(fats, "Fats cannot be negative.");
        Guard.AgainstNegative(carbohydrates, "Carbohydrates cannot be negative.");
        
        Calories = calories;
        Proteins = proteins;
        Fats = fats;
        Carbohydrates = carbohydrates;
    }
    
    public static Nutrition Zero => new Nutrition(0, 0, 0, 0);
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Calories;
        yield return Proteins;
        yield return Fats;
        yield return Carbohydrates;
    }
}