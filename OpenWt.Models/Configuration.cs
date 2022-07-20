using OpenWt.Contracts.Models;
using OpenWt.Models.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace OpenWt.Models;

public static class Configuration
{
    public static void InstallModels(this IServiceCollection service)
    {
        service.AddSingleton<ISkillsModel, SkillsMockModel>();
        service.AddSingleton<IContactsModel, ContactsMockModel>();
    }
}