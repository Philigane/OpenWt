using System.Runtime.Serialization;

namespace OpenWt.Controllers.v1;

[Serializable]
public sealed class ContactException : Exception
{
    public ContactException(string message) : base(message) { }

    private ContactException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}