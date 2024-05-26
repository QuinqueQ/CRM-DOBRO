namespace WebApi.Controllers;

[EnsureNotBlocked]
[ApiController]
[Route("api/lead")]
public class LeadController(LeadService leadService) : Controller
{
    [Authorize(Roles = "Saler")]
    [HttpGet("leads")]
    public async Task<IActionResult> GetMyLeads()
    {
        var salerid = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var leads = await leadService.GetMyLeadsAsync(salerid);
        if (leads.Count == 0)
            return NoContent();
        return Ok(leads);
    }

    [Authorize(Roles = "Saler")]
    [HttpPost]
    public async Task<IActionResult> LeadCreating(LeadSetDTO newlead)
    {
        var salerId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        bool contactFound = await leadService.CreateLeadAsync(newlead, salerId);
        if (!contactFound)
            return NotFound();

        return Created();
    }

    [Authorize(Roles = "Saler")]
    [HttpPut("status/{leadid}")]
    public async Task<IActionResult> StatusUpdate(LeadStatus status, int leadid)
    {
       LeadGetDTO? lead = await leadService.ChangeLeadStatusAsync(leadid, status);
        if (lead == null)
            return NotFound();
        return Ok();
    }
}

