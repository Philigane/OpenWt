using OpenWt.Contracts.Entities;

namespace OpenWt.Contracts.Dtos;

public interface IContactDto : ICore
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Fullname { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string MobilePhoneNumber { get; set; }
    public IEnumerable<ISkillDto> Skills { get; set; }
}