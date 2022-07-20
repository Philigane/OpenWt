using System.Runtime.Serialization;

namespace OpenWt.Controllers.v1;

[Serializable]
public sealed class SkillException : Exception
{
    public SkillException(string message) : base(message) { }

    private SkillException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}