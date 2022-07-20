using OpenWt.Contracts.Entities;

namespace OpenWt.Contracts.Models;

public interface IContactsModel : IGetter<IContact>, IAddOrUpdate<IContact>, IDelete<IContact>
{
}