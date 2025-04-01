namespace KebabDelivery.Domain.Common;

public abstract class EntityBase<TId>
{
    public TId Id { get; protected set; } = default!;
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }

    protected EntityBase() { }

    protected EntityBase(TId id)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetUpdatedNow()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public override bool Equals(object? obj)
    {
        if(obj is not EntityBase<TId> other)
            return false;
        
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode()
    {
        return Id?.GetHashCode() ?? 0;
    }

    public static bool operator ==(EntityBase<TId>? left, EntityBase<TId>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(EntityBase<TId>? left, EntityBase<TId>? right)
    {
        return !Equals(left, right);
    }
}