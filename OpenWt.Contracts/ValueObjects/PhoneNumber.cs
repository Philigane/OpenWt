using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace OpenWt.Contracts.ValueObjects;

public record PhoneNumber : ValueObject<string>
{
    public PhoneNumber(string value) : base(value, Validate)
    {
    }

    private static void Validate(string s)
    {
        if (!Regex.IsMatch(s, @"^(?:(?:\+|00)33[\s.-]{0,3}(?:\(0\)[\s.-]{0,3})?|0)[1-9](?:(?:[\s.-]?\d{2}){4}|\d{2}(?:[\s.-]?\d{3}){2})$"))
            throw new PhoneNumberException($"Invalid phone number : {s}.");
    }

    public static implicit operator string(PhoneNumber d) => d.Value;
    public static implicit operator PhoneNumber(string b) => new(b);
}

[Serializable]
public sealed class PhoneNumberException : Exception
{
    public PhoneNumberException(string message) : base(message) { }

    private PhoneNumberException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}