using OpenWt.Contracts.Entities;

namespace OpenWt.Contracts.Models;

public interface ISkillsModel : IGetter<ISkill>, IAddOrUpdate<ISkill>, IDelete<ISkill>
{
}
