﻿namespace WebApi.Controllers
{
    [EnsureNotBlocked]
    [ApiController]
    [Route("api/user")]
    public class UserController(UserService userservice) : Controller
    {
        private readonly UserService _userservice = userservice;

        //[AllowAnonymous]
        //[HttpGet("new/admin")]
        //public async Task<IActionResult> AddAdmin()
        //{
        //    await _userservice.NewAdmin();
        //    return Ok();
        //}

        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<IActionResult> LogIn(string email, string password)
        { 
            var user = await _userservice.LogInUserAsync(email, password);
            if (user != null)
            {
                ClaimsPrincipal? principal = _userservice.LoginWithHttpContext(user);
                await HttpContext.SignInAsync(principal);

                return Ok();
            }

            return Unauthorized();

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<IActionResult> ShowAllUsers()
        {
            List<UserGetDTO> users = await _userservice.GetAllUsersAsync();
            if(users.Count == 0)
                return NoContent();
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
            bool userFound = await _userservice.DeleteUserAsync(id);
            if (!userFound)
                return NotFound();
            
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("role/{id}")]
        public async Task<IActionResult> RoleUpdate(int id, UserRole newRole)
        {
            bool userFound = await _userservice.ChangeRoleAsync(id, newRole);
            if(!userFound)
                return NotFound();

            return NoContent();
        }

        [Authorize]
        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword(string NewPassword)
        {
                int userId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            
                await _userservice.ChangePasswordAsync(userId, NewPassword);

            return Ok();
        }
    }
}


