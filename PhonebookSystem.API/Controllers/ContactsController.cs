using Microsoft.AspNetCore.Mvc;
using PhonebookSystem.Core.Entities;
using PhonebookSystem.Service.Interfaces;

namespace PhonebookSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts(string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return Ok(await _contactService.GetAllContactsAsync());
            }

            return Ok(await _contactService.SearchContactsAsync(search));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact(Contact contact)
        {
            var createdContact = await _contactService.AddContactAsync(contact);
            return CreatedAtAction(nameof(GetContact), new { id = createdContact.Id }, createdContact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            var success = await _contactService.UpdateContactAsync(contact);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var success = await _contactService.DeleteContactAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}