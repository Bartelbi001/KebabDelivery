using KebabDelivery.Domain.Exceptions;

namespace KebabDelivery.Domain.Guards;

public static class Guard
{
    public static void AgainstNull<T>(T? value, string message)
    {
        if (value == null)
            throw new DomainValidationException(message);
    }

    public static void AgainstNullOrWhiteSpace(string? value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainValidationException(message);
    }

    public static void AgainstNegative(decimal value, string message)
    {
        if (value < 0)
            throw new DomainValidationException(message);
    }

    public static void AgainstNonPositive(decimal value, string message)
    {
        if (value <= 0)
            throw new DomainValidationException(message);
    }

    public static void AgainstEqual<T>(T? value, T? other, string message)
    {
        if (EqualityComparer<T>.Default.Equals(value, other))
            throw new DomainValidationException(message);
    }

    public static void AgainstEnumOutOfRange<TEnum>(TEnum value, string message)
        where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), value))
            throw new DomainValidationException(message);
    }
}