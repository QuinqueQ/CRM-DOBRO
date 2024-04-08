using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_DOBRO.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : Controller
    {
        public ContactController()
        {

        }

        [Authorize(Roles = "Admin, Marketing")]
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return View();
        }

        [Authorize(Roles = "Saler")]
        [HttpGet("lead")]
        public async Task<IActionResult> GetLeads()
        {
            return View();
        }

        [Authorize(Roles = "Saler")]
        [HttpPost]
        public async Task<IActionResult> ContactCreate()
        {
            return View();
        }

        [Authorize(Roles = "Marketing, Saler")]
        [HttpPut]
        public async Task<IActionResult> ContactUpdate()
        {
            return View();
        }

        [Authorize(Roles = "Marketing")]
        [HttpPut("status")]
        public async Task<IActionResult> ContactStatusUpdate()
        {
            return View();
        }

    }
}
//    Контакт:
//- Просмотр всех контактов(доступно: админ, маркетолог)
//- Просмотр контактов со статусом Lead(доступно: продажник)
//- Создание контакта(доступно: маркетолог)
//- Изменение контакта(доступно: маркетолог, продажник)
//- Изменение статуса контакта(доступно: маркетолог)

