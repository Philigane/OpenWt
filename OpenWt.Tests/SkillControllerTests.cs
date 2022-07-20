using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenWt.Controllers.v1;
using Xunit;

namespace OpenWt.Tests;

public class SkillControllerTests : CoreControllerTests
{

    #region Get
    [Fact]
    public void GetSkill_WithoutAdd_ShouldReturnEmpty() => Assert.Empty(SkillCont.Get(new List<int>()));

    [Fact]
    public void GetSkill_WithIds_WithoutAdd_ShouldReturnEmpty() => Assert.Empty(SkillCont.Get(new List<int> { 1, 2, 3 }));

    [Fact]
    public void GetSkill_WithIds_WithAdded_ShouldReturnEmpty()
    {
        AddSkill_ShouldntThrow();
        var skills = SkillCont.Get(new List<int> { 1 }).ToList();
        Assert.NotEmpty(skills);
        Assert.Equal(1, skills.First().Id);
    }
    #endregion

    #region Add
    [Fact]
    public void AddSkill_ShouldntThrow() =>
        Assert.NotNull(SkillCont.AddOrUpdate(1, "aze", "rty"));

    [Fact]
    public void AddSkill_InParallel_ShouldntThrow()
    {
        Parallel.For(1, 11, (index) =>
        {
            Assert.NotNull(SkillCont.AddOrUpdate(index, "aze", "rty"));
        });
        var skills = SkillCont.Get(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }).ToList();
        Assert.Equal(10, skills.Count());
        for (var i = 1; i < 11; i++)
        {
            Assert.NotNull(skills.FirstOrDefault(x => x.Id == i));
        }
    }

    #region Contact
    [Fact]
    public void AddContactToSkill_NoSkill_ShouldThrow_SkillException() =>
        Assert.Throws<SkillException>(() => SkillCont.AddContacts(1, new List<int> { 1 }));
    [Fact]
    public void AddContactToSkill_NoContact_ShouldThrow_ContactException()
    {
        AddSkill_ShouldntThrow();
        Assert.Throws<ContactException>(() => SkillCont.AddContacts(1, new List<int> { 1 }));
    }

    [Fact]
    public void AddContactToSkill_InParallel_ShouldntThrow()
    {
        AddSkill_ShouldntThrow();
        Parallel.For(1, 11, (index) =>
        {
            ContactCont.AddOrUpdate(index, "aze", "rty", "azerty", "address", "test@gmail.com", "0123456789");
        });
        for(var i = 1; i < 11; i++)
        {
            SkillCont.AddContacts(1, new List<int> { i });
        }
        var skill = SkillCont.Get(new List<int> { 1 }).First();
        Assert.Equal(10, skill.Contacts.Count());
    }
    #endregion
    #endregion

    #region Update
    [Fact]
    public void AddedTwice_ShouldNotCreateAnother_ButUpdatePrevious()
    {
        AddSkill_ShouldntThrow();
        SkillCont.AddOrUpdate(1, "rty", "aze");
        var skills = SkillCont.Get(new List<int> { 1 }).ToList();
        Assert.Single(skills);
        var skill = skills.First();
        Assert.Equal("rty", skill.Name);
        Assert.Equal("aze", skill.Level);
    }
    #endregion

    #region Delete
    [Fact]
    public void DeleteSkill_ShouldntThrow() =>
        SkillCont.Delete(new List<int> { 1 });

    [Fact]
    public void DeleteSkill_AlreadyAdded_ShouldntThrow()
    {
        AddSkill_ShouldntThrow();
        Assert.Single(SkillCont.Get(new List<int> { 1 }));
        SkillCont.Delete(new List<int> { 1 });
        Assert.Empty(SkillCont.Get(new List<int> { 1 }));
    }

    [Fact]
    public void DeleteSkills_AlreadyAdded_ShouldntThrow()
    {
        AddSkill_ShouldntThrow();
        SkillCont.AddOrUpdate(2, "aze", "rty");
        Assert.Equal(2, SkillCont.Get(new List<int> { 1, 2 }).Count());
        SkillCont.Delete(new List<int> { 1, 2 });
        Assert.Empty(SkillCont.Get(new List<int> { 1, 2 }));
    }
    #endregion
}