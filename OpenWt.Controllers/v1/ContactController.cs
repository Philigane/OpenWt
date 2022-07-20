using Microsoft.AspNetCore.Mvc;
using OpenWt.Controllers.v1.Dtos;
using OpenWt.Contracts.Dtos;
using OpenWt.Contracts.Entities;
using OpenWt.Contracts.Models;

namespace OpenWt.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1.0/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactsModel _contacts;
    private readonly ISkillsModel _skills;

    public ContactController(IContactsModel contacts, ISkillsModel skills)
    {
        _contacts = contacts;
        _skills = skills;
    }

    /// <summary>
    /// Get contacts
    /// </summary>
    /// <returns>List of contacts based on ids</returns>
    [HttpGet("Get")]
    [ProducesResponseType(typeof(IEnumerable<ISkillDto>), 200)]
    public IEnumerable<IContactDto> Get([FromQuery] List<int> ids) => _contacts.Get(x => !ids.Any() || ids.Contains(x.Id)).Select(x => new ContactDto(x));

    /// <summary>
    /// Add or update a contact.
    /// </summary>
    /// <param name="id">Id. May be 0 if it's an addition</param>
    /// <param name="firstname">Firstname</param>
    /// <param name="lastname">Lastname</param>
    /// <param name="fullname">Fullname</param>
    /// <param name="address">Address</param>
    /// <param name="email">Email</param>
    /// <param name="mobilePhoneNumber">Mobile Phone number</param>
    /// <returns>Added or updated contact</returns>
    [HttpPost("AddOrUpdate")]
    [ProducesResponseType(typeof(ISkillDto), 200)]
    public IContactDto AddOrUpdate([FromForm] int id, 
        [FromForm] string firstname,
        [FromForm] string lastname,
        [FromForm] string fullname,
        [FromForm] string address,
        [FromForm] string email,
        [FromForm] string mobilePhoneNumber)
    {
        var contact = _contacts.GetNew();
        contact.Id = id;
        contact.Firstname = firstname;
        contact.Lastname = lastname;
        contact.Fullname = fullname;
        contact.Address = address;
        contact.Email = email;
        contact.MobilePhoneNumber = mobilePhoneNumber;
        return new ContactDto(_contacts.AddOrUpdate(contact));
    }

    /// <summary>
    /// Add skills to a Contact
    /// </summary>
    /// <param name="id">Contact id</param>
    /// <param name="skillsIds">Skills Ids</param>
    [HttpPost("AddSkills")]
    [ProducesResponseType(200)]
    public void AddSkills([FromForm] int id, [FromForm] List<int> skillsIds)
    {
        if (_contacts.Get(x => x.Id == id).FirstOrDefault() is not { } contact)
            throw new ContactException($"Contact {id} unknown.");
        var skills = new List<ISkill>();
        foreach (var skillId in skillsIds)
        {
            if (_skills.Get(x => x.Id == skillId).FirstOrDefault() is not { } skill)
                throw new SkillException($"Skill {skillId} unknown.");
            skills.Add(skill);
        }
 
        contact.Skills = contact.Skills == null ? skills : contact.Skills.Concat(skills);

        _contacts.AddOrUpdate(contact);
    }

    /// <summary>
    /// Delete contacts
    /// </summary>
    /// <returns>200 HttpReponse</returns>
    [HttpGet("Delete")]
    [ProducesResponseType(00)]
    public void Delete([FromQuery] List<int> ids) => _contacts.Delete(_contacts.Get(x => ids.Contains(x.Id)));
}