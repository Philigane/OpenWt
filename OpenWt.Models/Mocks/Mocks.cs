using OpenWt.Contracts.Entities;
using OpenWt.Contracts.Models;
using OpenWt.Models.Entities;

namespace OpenWt.Models.Mocks;

public class ContactsMockModel : DatabaseCoreMock<IContact, Contact>, IContactsModel
{
}
public class SkillsMockModel : DatabaseCoreMock<ISkill, Skill>, ISkillsModel
{
}