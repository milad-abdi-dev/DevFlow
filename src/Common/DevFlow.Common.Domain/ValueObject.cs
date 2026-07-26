namespace DevFlow.Common.Domain;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right) =>
        !(left == right);

    public virtual bool Equals(ValueObject? other) =>
        other is not null &&
        GetType() == other.GetType() &&
        ValuesAreEqual(other);

    public override bool Equals(object? obj) =>
        obj is ValueObject other && Equals(other);

    public override int GetHashCode() =>
        GetAtomicValues().Aggregate(
            default(int),
            (hashCode, value) => HashCode.Combine(hashCode, value));

    protected abstract IEnumerable<object?> GetAtomicValues();

    private bool ValuesAreEqual(ValueObject other) =>
        GetAtomicValues().SequenceEqual(other.GetAtomicValues());
}
