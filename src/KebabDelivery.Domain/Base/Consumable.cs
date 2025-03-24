using KebabDelivery.Domain.Exceptions;

namespace KebabDelivery.Domain.Base;

public abstract class Consumable
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public bool IsAlcoholic { get; protected set; }
    public bool ContainsLactose { get; protected set; }
    public decimal Calories { get; protected set; }
    public decimal Proteins { get; protected set; }
    public decimal Fats { get; protected set; }
    public decimal Carbohydrates { get; protected set; }

    protected Consumable(string name, bool isAlcoholic, bool containsLactose, decimal calories, decimal proteins, decimal fats, decimal carbohydrates)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("The name cannot be empty.");

        if (calories < 0 || proteins < 0 || fats < 0 || carbohydrates < 0)
            throw new DomainValidationException("BJU values cannot be negative.");

        Id = Guid.NewGuid();
        Name = name;
        IsAlcoholic = isAlcoholic;
        ContainsLactose = containsLactose;
        Calories = calories;
        Proteins = proteins;
        Fats = fats;
        Carbohydrates = carbohydrates;
    }
    
    protected void UpdateNutrition(decimal calories, decimal proteins, decimal fats, decimal carbohydrates)
    {
        if (calories < 0 || proteins < 0 || fats < 0 || carbohydrates < 0)
            throw new DomainValidationException("BJU values cannot be negative.");

        Calories = calories;
        Proteins = proteins;
        Fats = fats;
        Carbohydrates = carbohydrates;
    }
}