namespace WebApi.Controllers;

[EnsureNotBlocked]
[ApiController]
[Route("api/contact")]
public class ContactController(ContactService contactService) : Controller
{

    [Authorize(Roles = "Admin, Marketing")]
    [HttpGet("contacts")]
    public async Task<IActionResult> GetContacts()
    {
       var contacts = await contactService.GetContactsAsync();
        if (contacts.Count == 0)
            return NoContent();

        return Ok(contacts);
    }

    [Authorize(Roles = "Saler")]
    [HttpGet("leads")]
    public async Task<IActionResult> GetLeads()
    {
        var leads = await contactService.GetContactLeadsAsync();
        if(leads.Count == 0)
            return NoContent();
       
        return Ok(leads);
    }

    [Authorize(Roles = "Marketing, Admin")]
    [HttpPost]
    public async Task<IActionResult> ContactCreate(ContactSetDTO contact)
    {
        int marketingId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        await contactService.CreateContactAsync(contact, marketingId);
        return Created();

    }

    [Authorize(Roles = "Marketing, Saler")]
    [HttpPut("{contactid}")]
    public async Task<IActionResult> ContactUpdate(ContactSetDTO contact, int contactid)
    {
        await contactService.ContactChangeAsync(contact, contactid);
        return Ok();
    }

    [Authorize(Roles = "Marketing")]
    [HttpPut("status/{contactid}")]
    public async Task<IActionResult> ContactStatusUpdate (ContactStatus status, int contactid)
    {
        await contactService.ContactChangeStatusAsync(status, contactid);
        return Ok();
    }
}

