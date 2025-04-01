using KebabDelivery.Domain.Common;
using KebabDelivery.Domain.Exceptions;
using KebabDelivery.Domain.Guards;
using KebabDelivery.Domain.ValueObjects;

namespace KebabDelivery.Domain.Base;

public abstract class Consumable : EntityBase<Guid>
{
    public string Name { get; private set; }
    public bool IsAlcoholic { get; protected set; }
    public bool ContainsLactose { get; protected set; }
    public Nutrition Nutrition { get; protected set; }
    
    protected Consumable() { }

    protected Consumable(string name, bool isAlcoholic, bool containsLactose, Nutrition nutrition)
        : base(Guid.NewGuid())
    {
        Guard.AgainstNullOrWhiteSpace(name, "Name is required.");
        Guard.AgainstNull(nutrition, "Nutrition is required.");
        
        Name = name;
        IsAlcoholic = isAlcoholic;
        ContainsLactose = containsLactose;
        Nutrition = nutrition;
    }

    protected void UpdateNutrition(Nutrition newNutrition)
    {
        Guard.AgainstNull(newNutrition, "Nutrition is required.");
        Nutrition = newNutrition;
        SetUpdatedNow();
    }
}