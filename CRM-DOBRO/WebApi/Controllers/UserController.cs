namespace WebApi.Controllers;


[ApiController]
[Route("api/user")]
public class UserController(UserService userservice) : Controller
{
    //Admin
   //[AllowAnonymous]
   //[HttpGet("new/admin")]
   // public async Task<IActionResult> AddAdmin()
   // {
   //     await userservice.NewAdmin();
   //     return Ok();
   // }

    [AllowAnonymous]
    [HttpGet("login")]
    public async Task<IActionResult> LogIn(string email, string password)
    { 
        var user = await userservice.LogInUserAsync(email, password);
        if (user != null)
        {
            ClaimsPrincipal? principal = UserService.LoginWithHttpContext(user);
            await HttpContext.SignInAsync(principal);

            return Ok();
        }

        return Unauthorized();

    }
    [EnsureNotBlocked]
    [Authorize(Roles = "Admin")]
    [HttpGet("users")]
    public async Task<IActionResult> ShowAllUsers()
    {
        List<UserGetDTO> users = await userservice.GetAllUsersAsync();
        if(users.Count == 0)
            return NoContent();
        return Ok(users);
    }
    [EnsureNotBlocked]
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

    [EnsureNotBlocked]
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> UserCreate(UserSetDTO user)
    {
        await userservice.CreateNewUserAsync(user);

        return Created();
    }

    [EnsureNotBlocked]
    [Authorize(Roles = "Admin")]
    [HttpPut("ban/{id}")]
    public async Task<IActionResult> UserBan(int id)
    {
      var user = await userservice.BanUserAsync(id);
        if (user == null)
            return NotFound();

        return Ok();
    }

    [EnsureNotBlocked]
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> UserDeleteById(int id)
    {
        bool userFound = await userservice.DeleteUserAsync(id);
        if (!userFound)
            return NotFound();
        
        return NoContent();
    }

    [EnsureNotBlocked]
    [Authorize(Roles = "Admin")]
    [HttpPut("role/{id}")]
    public async Task<IActionResult> RoleUpdate(int id, UserRole newRole)
    {
        bool userFound = await userservice.ChangeRoleAsync(id, newRole);
        if(!userFound)
            return NotFound();

        return NoContent();
    }

    [EnsureNotBlocked]
    [Authorize]
    [HttpPut("password")]
    public async Task<IActionResult> ChangePassword(string NewPassword)
    {
            int userId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        
            await userservice.ChangePasswordAsync(userId, NewPassword);

        return Ok();
    }
}


