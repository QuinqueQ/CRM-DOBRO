using CRM_DOBRO.DTOs;
using CRM_DOBRO.Entities;
using CRM_DOBRO.Enums;
using CRM_DOBRO.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<IActionResult> LogIn(string email, string password)
        { 
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return Unauthorized();

            var user = await _userservice.LogInUserAsync(email, password);
            if (user != null)
            {
                await LoginWithHttpContext(user);

                return Ok();
            }

            return Unauthorized();

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<IActionResult> ShowAllUsers()
        {
            List<UserGetDTO> users = await _userservice.GetAllUsersAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("details")]
        public IActionResult UserShowDetails()
        {
            var user = HttpContext.User;
            var userDetails = new
            {
                FullName = user.Identity?.Name,
                Email = user.FindFirst(ClaimTypes.Email)?.Value,
                Role = user.FindFirst(ClaimTypes.Role)?.Value,
                DateOfBan = user.FindFirst("DateOfBan")?.Value
            };

            return Ok(userDetails);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UserCreate(UserSetDTO user)
        {
            if (string.IsNullOrWhiteSpace(user.FullName)
                || user.FullName == "string"
                || string.IsNullOrWhiteSpace(user.Password)
                || user.Password == "string"
                || string.IsNullOrWhiteSpace(user.Email)
                || user.Email == "string")
            {
                return BadRequest();
            }

            await _userservice.CreateNewUserAsync(user);
            return Created();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("ban/{id}")]
        public async Task<IActionResult> UserBan(int id)
        {
          var user = await _userservice.BanUserAsync(id);
            if (user == null)
                return NotFound();

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> UserDeleteById(int id)
        {
            await _userservice.DeleteUserAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("role/{id}")]
        public async Task<IActionResult> RoleUpdate(int id, UserRole newRole)
        {
            await _userservice.ChangeRoleAsync(id, newRole);
            return NoContent();
        }

        [EnsureNotBlocked]
        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword(string NewPassword)
        {
            if (string.IsNullOrWhiteSpace(NewPassword) || string.IsNullOrWhiteSpace(NewPassword))
                return BadRequest();

                int userId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            
                await _userservice.ChangePasswordAsync(userId, NewPassword);

            return Ok();
        }

        private Task LoginWithHttpContext(User user)
        {
            var claims = new Claim[]
            {
            new ("guid", Guid.NewGuid().ToString()),
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.FullName),
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.Role, user.Role.ToString()),
            new ("DateOfBan", user.BlockingDate.ToString() ?? "")
            
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            return HttpContext.SignInAsync(principal);
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

