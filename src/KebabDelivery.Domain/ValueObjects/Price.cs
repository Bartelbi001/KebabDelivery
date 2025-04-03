using KebabDelivery.Domain.Common;
using KebabDelivery.Domain.Guards;

namespace KebabDelivery.Domain.ValueObjects;

public class Price : ValueObject
{
    public int Cents { get; }
    public string Currency { get; }

    public decimal Amount => Cents / 100m;

    public Price(int cents, string currency)
    {
        Guard.AgainstNonPositive(cents, "Price must be greater than zero.");
        Guard.AgainstNullOrWhiteSpace(currency, "Currency is required.");
        
        Cents = cents;
        Currency = currency.ToUpperInvariant();
    }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Cents;
        yield return Currency;
    }
}