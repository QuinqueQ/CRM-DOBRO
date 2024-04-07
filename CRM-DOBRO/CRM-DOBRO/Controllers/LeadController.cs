using Microsoft.AspNetCore.Mvc;

namespace CRM_DOBRO.Controllers
{
    [ApiController]
    [Route("api/lead")]
    public class LeadController : Controller
    {
        public LeadController()
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> GetLeads()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LeadCreating()
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> StatusUpdate()
        {
            return Ok();
        }

    }
}
//Лид:
//-Просмотр своих лидов(доступно: продажник)
//- Создание лида на основе контакта (доступно: продажник)
//- Изменение статуса лида (доступно: продажник)
