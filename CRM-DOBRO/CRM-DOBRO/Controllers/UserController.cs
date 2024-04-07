using CRM_DOBRO.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM_DOBRO.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly UserService _userservice;
        public UserController(UserService userservice)
        {
            _userservice = userservice;
        }

        [HttpGet]
        public async Task<IActionResult> SingIn()
        {
            return Ok();
        }

        [HttpGet("users")]
        public async Task<IActionResult> ShowAllUsers()
        {
            return  Ok();
        }

        [HttpGet("details")]
        public async Task<IActionResult> UserShowDetails()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UserCreate()
        {
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UserBan()
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> UserDelete()
        {
            return NotFound();
        }

        [HttpPut("role")]
        public async Task<IActionResult> RoleUpdate()
        {
            return Ok();
        }

        [HttpPut("password")]
        public async Task<IActionResult> PasswordUpdate()
        {
            return Ok();
        }

    }
}
//    Пользователь:
//- Вход в систему(доступно: анонимно)
//- Просмотр пользователей(доступно: админ)
//- Просмотр данных пользователя(доступно: сам пользователь)
//- Создание, блокировка и удаление пользователя(доступно: админ)
//- Изменение роли пользователя(доступно: админ)
//- Изменение пароля пользователя(доступно: сам пользователь)

