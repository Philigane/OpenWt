namespace OpenWt.Contracts.ValueObjects;

public record ValueObject<T>
{
    protected T Value;

    protected ValueObject(T value)
    {
        Value = value;
    }

    protected ValueObject(T value, Action<T> validate) : this(value)
    {
        validate(value);
    }

    public override string ToString() => Value?.ToString() ?? "";
}