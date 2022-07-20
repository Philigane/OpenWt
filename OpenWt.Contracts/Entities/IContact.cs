using OpenWt.Contracts.ValueObjects;

namespace OpenWt.Contracts.Entities;

public interface IContact : ICore
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Fullname { get; set; }
    public string Address { get; set; }
    public Email Email { get; set; }
    public PhoneNumber MobilePhoneNumber { get; set; }
    public IEnumerable<ISkill>? Skills { get; set; }
}