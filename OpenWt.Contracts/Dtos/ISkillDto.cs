using OpenWt.Contracts.Entities;

namespace OpenWt.Contracts.Dtos;

public interface ISkillDto : ICore
{
    public string Name { get; set; }
    public string Level { get; set; }
    public IEnumerable<IContactDto> Contacts { get; set; }
}