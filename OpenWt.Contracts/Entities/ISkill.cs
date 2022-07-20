namespace OpenWt.Contracts.Entities;

public interface ISkill : ICore
{
    public string Name { get; set; }
    public string Level { get; set; }
    public IEnumerable<IContact>? Contacts { get; set; }
}