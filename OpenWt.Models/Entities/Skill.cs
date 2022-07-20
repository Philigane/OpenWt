using OpenWt.Contracts.Entities;

namespace OpenWt.Models.Entities;

public class Skill : Core, ISkill
{
    public string Name { get; set; }
    public string Level { get; set; }
    public IEnumerable<IContact>? Contacts { get; set; }
}
