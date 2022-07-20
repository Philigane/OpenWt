using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace OpenWt.Contracts.ValueObjects;

public record Email : ValueObject<string>
{
    public Email(string value) : base(value, Validate)
    {
    }

    private static void Validate(string s)
    {
        if (!new EmailAddressAttribute().IsValid(s))
            throw new EmailException($"Invalid email : {s}.");
    }

    public static implicit operator string(Email d) => d.Value;
    public static implicit operator Email(string b) => new(b);
}

[Serializable]
public sealed class EmailException : Exception
{
    public EmailException(string message) : base(message) { }

    private EmailException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}