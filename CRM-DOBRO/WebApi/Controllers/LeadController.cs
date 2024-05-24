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
    [Route("api/lead")]
    public class LeadController(LeadService leadService) : Controller
    {
        private readonly LeadService _leadService = leadService;

        [Authorize(Roles = "Saler")]
        [HttpGet("leads")]
        public async Task<IActionResult> GetMyLeads()
        {
            var salerid = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var leads = await _leadService.GetMyLeadsAsync(salerid);
            if (leads.Count == 0)
                return NoContent();
            return Ok(leads);
        }

        [Authorize(Roles = "Saler")]
        [HttpPost]
        public async Task<IActionResult> LeadCreating(LeadSetDTO newlead)
        {
            var salerId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool contactFound = await _leadService.CreateLeadAsync(newlead, salerId);
            if (!contactFound)
                return NotFound();

            return Created();
        }

        [Authorize(Roles = "Saler")]
        [HttpPut("status/{leadid}")]
        public async Task<IActionResult> StatusUpdate(LeadStatus status, int leadid)
        {
           LeadGetDTO? lead = await _leadService.ChangeLeadStatusAsync(leadid, status);
            if (lead == null)
                return NotFound();
            return Ok();
        }
    }
}

