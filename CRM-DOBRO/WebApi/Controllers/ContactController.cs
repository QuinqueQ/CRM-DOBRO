using WebApi.CustomAttributes;
using Application.Contracts;
using Domain.Enums;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [EnsureNotBlocked]
    [ApiController]
    [Route("api/contact")]
    public class ContactController(ContactService contactService) : Controller
    {
        private readonly ContactService _contactService = contactService;

        [Authorize(Roles = "Admin, Marketing")]
        [HttpGet("contacts")]
        public async Task<IActionResult> GetContacts()
        {
           var contacts = await _contactService.GetContactsAsync();
            if (contacts.Count == 0)
                return NoContent();

            return Ok(contacts);
        }

        [Authorize(Roles = "Saler")]
        [HttpGet("leads")]
        public async Task<IActionResult> GetLeads()
        {
            var leads = await _contactService.GetContactLeadsAsync();
            if(leads.Count == 0)
                return NoContent();
           
            return Ok(leads);
        }

        [Authorize(Roles = "Marketing")]
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

