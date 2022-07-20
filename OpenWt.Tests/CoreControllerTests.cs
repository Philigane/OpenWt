using System;
using Microsoft.Extensions.DependencyInjection;
using OpenWt.Controllers.v1;
using OpenWt.Models;

namespace OpenWt.Tests;

public class CoreControllerTests
{
    protected SkillController SkillCont { get; set; }
    protected ContactController ContactCont { get; set; }

    public CoreControllerTests()
    {
        var container = new ServiceCollection();
        container.InstallModels();
        container.AddScoped<ContactController>();
        container.AddScoped<SkillController>();
        if (container.BuildServiceProvider() is not { } builder)
            throw new Exception("Can't create the builder");
        if (builder.GetService<SkillController>() is not { } skillCont)
            throw new Exception("Can't get the SkillController");
        if (builder.GetService<ContactController>() is not { } contactCont)
            throw new Exception("Can't get the ContactController");

        SkillCont = skillCont;
        ContactCont = contactCont;
    }
}