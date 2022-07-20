using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenWt.Contracts.ValueObjects;
using OpenWt.Controllers.v1;
using Xunit;

namespace OpenWt.Tests;

public class ContactControllerTests : CoreControllerTests
{
    #region Get
    [Fact]
    public void GetContact_WithoutAdd_ShouldReturnEmpty() => Assert.Empty(ContactCont.Get(new List<int>()));

    [Fact]
    public void GetContact_WithIds_WithoutAdd_ShouldReturnEmpty() => Assert.Empty(ContactCont.Get(new List<int>{1, 2, 3}));

    [Fact]
    public void GetContact_WithIds_WithAdded_ShouldReturnEmpty()
    {
        AddContact_ShouldntThrow();
        var contacts = ContactCont.Get(new List<int> { 1 }).ToList();
        Assert.NotEmpty(contacts);
        Assert.Equal(1, contacts.First().Id);
    }
    #endregion

    #region Add
    [Fact]
    public void AddContact_ShouldntThrow() => 
        Assert.NotNull(ContactCont.AddOrUpdate(1, "aze", "rty", "azerty", "address", "test@gmail.com", "0123456789"));

    [Fact]
    public void AddContact_InParallel_ShouldntThrow()
    {
        Parallel.For(1, 11, (index) =>
        {
            Assert.NotNull(ContactCont.AddOrUpdate(index, "aze", "rty", "azerty", "address", "test@gmail.com", "0123456789"));
        });
        var contacts = ContactCont.Get(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }).ToList();
        Assert.Equal(10, contacts.Count());
        for (var i = 1; i < 11; i++)
        {
            Assert.NotNull(contacts.FirstOrDefault(x => x.Id == i));
        }
    }

    [Theory]
    [InlineData("totogmail.com")]
    [InlineData("totooutlook.com")]
    [InlineData("gmailcom")]
    [InlineData("123")]
    [InlineData("123azerty")]
    public void AddContact_InvalidEmail_ShouldThrow_EmailException(string email) =>
        Assert.Throws<EmailException>(() => 
            ContactCont.AddOrUpdate(1, "aze", "rty", "azerty", "address", email, "0123456789"));

    [Theory, InlineData("012345678"), InlineData("01 23 45 67 8"), InlineData("01.23.45.67.8"),
     InlineData("0123 45.67.8"), InlineData("0033 123-456-89"), InlineData("+33-1.23.45.6.89"),
     InlineData("+33 - 123 46 789"), InlineData("+33(0) 123 56 789"), InlineData("+33 (0)123 5 67 89"),
     InlineData("+33 (0)1 235-6789"), InlineData("+33(0) - 12456789"), InlineData("01234567891"),
     InlineData("01 23 45 67 89 1"), InlineData("01.23.45.67.89.1"), InlineData("0123 45.67.89.1"),
     InlineData("0033 123-456-8991"), InlineData("+33-1.23.45.6.8991"), InlineData("+33 - 123 46 78991"),
     InlineData("+33(0) 123 56 78991"), InlineData("+33 (0)123 5 67 8991"), InlineData("+33 (0)1 235-678991"),
     InlineData("+33(0) - 1245678991")]
    public void AddContact_InvalidPhoneNumber_ShouldThrow_PhoneNumberException(string phoneNumber) =>
        Assert.Throws<PhoneNumberException>(() => 
            ContactCont.AddOrUpdate(1, "aze", "rty", "azerty", "address", "test@gmail.com", phoneNumber));

    #region Skills
    [Fact]
    public void AddSkillToContact_NoContact_ShouldThrow_ContactException() => 
        Assert.Throws<ContactException>(() => ContactCont.AddSkills(1, new List<int>{1}));
    [Fact]
    public void AddSkillToContact_NoSkill_ShouldThrow_SkillException()
    {
        AddContact_ShouldntThrow();
        Assert.Throws<SkillException>(() => ContactCont.AddSkills(1, new List<int> { 1 }));
    }

    [Fact]
    public void AddSkillToContact_InParallel_ShouldntThrow()
    {
        AddContact_ShouldntThrow();
        Parallel.For(1, 11, (index) =>
        {
            SkillCont.AddOrUpdate(index, "test", "level");
        });
        for (var i = 1; i < 11; i++)
        {
            ContactCont.AddSkills(1, new List<int> { i });
        }
        var contact = ContactCont.Get(new List<int> { 1 }).First();
        Assert.Equal(10, contact.Skills.Count());
    }
    #endregion
    #endregion

    #region Update
    [Fact]
    public void AddedTwice_ShouldNotCreateAnother_ButUpdatePrevious()
    {
        AddContact_ShouldntThrow();
        ContactCont.AddOrUpdate(1, "rty", "aze", "rtyaze", "addresses", "tests@gmail.com", "0623456789");
        var contacts = ContactCont.Get(new List<int> { 1 }).ToList();
        Assert.Single(contacts);
        var contact = contacts.First();
        Assert.Equal("rty", contact.Firstname);
        Assert.Equal("aze", contact.Lastname);
        Assert.Equal("rtyaze", contact.Fullname);
        Assert.Equal("addresses", contact.Address);
        Assert.Equal("tests@gmail.com", contact.Email);
        Assert.Equal("0623456789", contact.MobilePhoneNumber);
    }
    #endregion

    #region Delete
    [Fact]
    public void DeleteContact_ShouldntThrow() =>
        ContactCont.Delete(new List<int>{ 1 });

    [Fact]
    public void DeleteContact_AlreadyAdded_ShouldntThrow()
    {
        AddContact_ShouldntThrow();
        Assert.Single(ContactCont.Get(new List<int> { 1 }));
        ContactCont.Delete(new List<int> { 1 });
        Assert.Empty(ContactCont.Get(new List<int> { 1 }));
    }

    [Fact]
    public void DeleteContacts_AlreadyAdded_ShouldntThrow()
    {
        AddContact_ShouldntThrow();
        ContactCont.AddOrUpdate(2, "aze", "rty", "azerty", "address", "test@gmail.com", "0123456789");
        Assert.Equal(2, ContactCont.Get(new List<int> { 1, 2 }).Count());
        ContactCont.Delete(new List<int> { 1, 2 });
        Assert.Empty(ContactCont.Get(new List<int> { 1, 2 }));
    }
    #endregion
}