using CRM_DOBRO.DTOs;
using CRM_DOBRO.Entities;
using CRM_DOBRO.Enums;
using CRM_DOBRO.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRM_DOBRO.Controllers
{
    [ApiController]
    [Route("api/lead")]
    public class LeadController : Controller
    {
        private readonly LeadService _leadService;

        public LeadController(LeadService leadService)
        {
            this._leadService = leadService;
        }

        [Authorize(Roles = "Saler")]
        [HttpGet]
        public async Task<IActionResult> GetLeads()
        {
            var salerid = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var leads = await _leadService.GetLeadsAsync(salerid);
            return Ok(leads);
        }

        [Authorize(Roles = "Saler")]
        [HttpPost("{contactId}")]
        public async Task<IActionResult> LeadCreating(LeadSetDTO newlead, int contactId)
        {
            var salerId = Convert.ToInt32(HttpContext.User.FindAll(ClaimTypes.NameIdentifier));
            await _leadService.CreateLeadAsync(newlead,contactId, salerId);

            return Created();
        }

        [Authorize(Roles = "Saler")]
        [HttpPut]
        public async Task<IActionResult> StatusUpdate(LeadStatus status, int leadid)
        {
           Lead? lead = await _leadService.ChangeLeadStatusAsync(leadid, status);
            if (lead == null)
                return NoContent();
            return Ok();
        }

    }
}
//Лид:
//-Просмотр своих лидов(доступно: продажник)
//- Создание лида на основе контакта (доступно: продажник)
//- Изменение статуса лида (доступно: продажник)
