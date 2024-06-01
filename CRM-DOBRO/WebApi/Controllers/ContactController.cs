using Domain.Result.ErrorMessage;

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
       var resultContacts = await contactService.GetAllAsync();

        if (resultContacts.Value.Count == 0)
            return NoContent();

        return Ok(resultContacts.Value);
    }

    [Authorize(Roles = "Saler")]
    [HttpGet("leads")]
    public async Task<IActionResult> GetLeads()
    {
        var resultLeads = await contactService.GetLeadsAsync();
        if(resultLeads.Value.Count == 0)
            return NoContent();
       
        return Ok(resultLeads.Value);
    }

    [Authorize(Roles = "Marketing")]
    [HttpPost]
    public async Task<IActionResult> Create(ContactSetDTO contact)
    {
        int marketingId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        await contactService.CreateContactAsync(contact, marketingId);
        return Created();

    }

    [Authorize(Roles = "Marketing, Saler")]
    [HttpPut("{contactid}")]
    public async Task<IActionResult> Update(ContactSetDTO contact, int contactid)
    {
        var result = await contactService.ContactUpdateAsync(contact, contactid);
        return result.Match(
            onSuccess: value => Ok(result.Value),
            onFailure: error =>
            {
                if (error.Code == ContactErrors.IdNotFound)
                    return NotFound();
                return BadRequest(result.Error);
            });



    }

    [Authorize(Roles = "Marketing")]
    [HttpPut("status/{contactid}")]
    public async Task<IActionResult> StatusUpdate(ContactStatus status, int contactid)
    {
        var result = await contactService.ContactChangeStatusAsync(status, contactid);

        return result.Match(
            onSuccess: value => Ok(),
            onFailure: error =>
            { if(error.Code == ContactErrors.IdNotFound)
                    return NotFound();

                return BadRequest(result.Error);
            });
    }
}

