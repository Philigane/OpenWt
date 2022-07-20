using OpenWt.Contracts.Dtos;
using OpenWt.Contracts.Entities;

namespace OpenWt.Controllers.v1.Dtos;

public class ContactDto : IContactDto
{
    public ContactDto(IContact contact)
    {
        Id = contact.Id;
        Firstname = contact.Firstname;
        Lastname = contact.Lastname;
        Fullname = contact.Fullname;
        Address = contact.Address;
        Email = contact.Email;
        MobilePhoneNumber = contact.MobilePhoneNumber;
        Skills = contact.Skills == null ? new List<ISkillDto>() : contact.Skills.Select(x => new SkillDto(x));
    }

    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Fullname { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string MobilePhoneNumber { get; set; }
    public IEnumerable<ISkillDto> Skills { get; set; }
}