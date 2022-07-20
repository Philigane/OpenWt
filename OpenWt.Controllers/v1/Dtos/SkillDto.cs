using OpenWt.Contracts.Dtos;
using OpenWt.Contracts.Entities;

namespace OpenWt.Controllers.v1.Dtos;

public class SkillDto : ISkillDto
{
    public SkillDto(ISkill skill)
    {
        Id = skill.Id;
        Name = skill.Name;
        Level = skill.Level;
        Contacts = skill.Contacts == null ? new List<IContactDto>() : skill.Contacts.Select(x => new ContactDto(x));
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Level { get; set; }
    public IEnumerable<IContactDto> Contacts { get; set; }
}