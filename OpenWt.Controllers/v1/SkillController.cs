using Microsoft.AspNetCore.Mvc;
using OpenWt.Controllers.v1.Dtos;
using OpenWt.Contracts.Dtos;
using OpenWt.Contracts.Entities;
using OpenWt.Contracts.Models;

namespace OpenWt.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1.0/[controller]")]
public class SkillController : ControllerBase
{
    private readonly ISkillsModel _skills;
    private readonly IContactsModel _contacts;

    public SkillController(ISkillsModel skills, IContactsModel contacts)
    {
        _skills = skills;
        _contacts = contacts;
    }

    /// <summary>
    /// Get skills
    /// </summary>
    /// <returns>List of skills based on ids</returns>
    [HttpGet("Get")]
    [ProducesResponseType(typeof(IEnumerable<ISkillDto>), 200)]
    public IEnumerable<ISkillDto> Get([FromQuery] List<int> ids) => _skills.Get(x => !ids.Any() || ids.Contains(x.Id)).Select(x => new SkillDto(x));

    /// <summary>
    /// Add or update a skill.
    /// </summary>
    /// <param name="id">Id. May be 0 if it's an addition</param>
    /// <param name="name">Skill name</param>
    /// <param name="level">Level (expertise)</param>
    /// <returns>Added or updated skill</returns>
    [HttpPost("AddOrUpdate")]
    [ProducesResponseType(typeof(ISkillDto), 200)]
    public ISkillDto AddOrUpdate([FromForm] int id,
        [FromForm] string name,
        [FromForm] string level)
    {
        var skill = _skills.GetNew();
        skill.Id = id;
        skill.Name = name;
        skill.Level = level;
        return new SkillDto(_skills.AddOrUpdate(skill));
    }

    /// <summary>
    /// Add contacts to a Skill
    /// </summary>
    /// <param name="id">Skill id</param>
    /// <param name="contactsIds">Contacts Ids</param>
    [HttpPost("AddContacts")]
    [ProducesResponseType(200)]
    public void AddContacts([FromForm] int id, [FromForm] List<int> contactsIds)
    {
        if (_skills.Get(x => x.Id == id).FirstOrDefault() is not { } skill)
            throw new SkillException($"Skill {id} unknown.");
        var contacts = new List<IContact>();
        foreach (var contactId in contactsIds)
        {
            if (_contacts.Get(x => x.Id == contactId).FirstOrDefault() is not { } contact)
                throw new ContactException($"Contact {contactId} unknown.");
            contacts.Add(contact);
        }

        skill.Contacts = skill.Contacts == null ? contacts : skill.Contacts.Concat(contacts);

        _skills.AddOrUpdate(skill);
    }

    /// <summary>
    /// Delete skills
    /// </summary>
    /// <returns>401 response if everything is good.</returns>
    [HttpGet("Delete")]
    [ProducesResponseType(200)]
    public void Delete([FromQuery] List<int> ids) => _skills.Delete(_skills.Get(x => ids.Contains(x.Id)));
}