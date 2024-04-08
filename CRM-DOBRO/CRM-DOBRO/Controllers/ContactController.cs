using CRM_DOBRO.DTOs;
using CRM_DOBRO.Enums;
using CRM_DOBRO.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRM_DOBRO.Controllers
{
    [ApiController]
    [Route("api/contact")]
    [Authorize]
    public class ContactController : Controller
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            this._contactService = contactService;
        }

        [Authorize(Roles = "Admin, Marketing")]
        [HttpGet("contacts")]
        public async Task<IActionResult> GetContacts()
        {
           var contacts = await _contactService.GetContactsAsync();
            if (contacts == null || contacts.Count == 0)
                return NoContent();

            return Ok(contacts);
        }

        [Authorize(Roles = "Saler")]
        [HttpGet("leads")]
        public async Task<IActionResult> GetLeads()
        {
            var leads = await _contactService.GetLeadsAsync();
            if(leads == null || leads.Count == 0)
                return NoContent();
           
            return Ok(leads);
        }

        [Authorize(Roles = "Saler")]
        [HttpPost]
        public async Task<IActionResult> ContactCreate(ContactSetDTO contact)
        {
            int marketingId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _contactService.CreateContactAsync(contact, marketingId);
            return Created();

        }

        [Authorize(Roles = "Marketing, Saler")]
        [HttpPut("{contactid}")]
        public async Task<IActionResult> ContactUpdate(ContactSetDTO contact, int contactid)
        {
            await _contactService.ContactChangeAsync(contact, contactid);
            return Ok();
        }

        [Authorize(Roles = "Marketing")]
        [HttpPut("status/{contactid}")]
        public async Task<IActionResult> ContactStatusUpdate (ContactStatus status, int contactid)
        {
            await _contactService.ContactChangeStatusAsync(status, contactid);
            return Ok();
        }

    }
}
//    Контакт:
//- Просмотр всех контактов(доступно: админ, маркетолог)
//- Просмотр контактов со статусом Lead(доступно: продажник)
//- Создание контакта(доступно: маркетолог)
//- Изменение контакта(доступно: маркетолог, продажник)
//- Изменение статуса контакта(доступно: маркетолог)

