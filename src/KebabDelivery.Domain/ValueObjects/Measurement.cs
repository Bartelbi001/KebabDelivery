using KebabDelivery.Domain.Common;
using KebabDelivery.Domain.Enums;
using KebabDelivery.Domain.Guards;

namespace KebabDelivery.Domain.ValueObjects;

public class Measurement : ValueObject
{
    public Measurement(decimal amount, MeasurementUnit unit)
    {
        Guard.AgainstNonPositive(amount, "Amount must be greater than zero.");
        Guard.AgainstEnumOutOfRange(unit, "Invalid measurement unit.");

        Amount = amount;
        Unit = unit;
    }

    public decimal Amount { get; }
    public MeasurementUnit Unit { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Unit;
    }
}