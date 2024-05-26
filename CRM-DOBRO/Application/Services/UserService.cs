using Domain.Abstractions.Repositories;
using System.Security.Claims;

namespace Application.Services;

public class UserService(IUserRepository userRepository, IUnitOfWork uow)
{

    /// <summary>
    /// Method for creating the first admin
    /// </summary>
    /// <returns></returns>
    public async Task NewAdmin()
    {
        var Admin = new User
        {
            Email = "alonastq@gmail.com",
            FullName = "Mamedov Nizar",
            Password = "12345",
            Role = UserRole.Admin,
        };

        await userRepository.AddAsync(Admin);
        await uow.SaveChangesAsync();

    }
    public static ClaimsPrincipal LoginWithHttpContext(User user)
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
        ClaimsPrincipal principal = new (identity);

        return principal;
    }
    
    public async Task<User?> LogInUserAsync(string email, string password)
    {

        User? selectedUser = await userRepository.LogInAsync(email, password);
        return selectedUser!;
    }

    public async Task<List<UserGetDTO>> GetAllUsersAsync()
    {
        List<User> users = await userRepository.GetAllAsync();
        List<UserGetDTO> usersDTO = users.Adapt<List<UserGetDTO>>();

        return usersDTO;
    }

    public async Task CreateNewUserAsync(UserSetDTO newuser)
    {
        User user = newuser.Adapt<User>();

       await userRepository.AddAsync(user);
       await uow.SaveChangesAsync();
    }



    public async Task<User?> BanUserAsync(int id)
    {
        User? user = await userRepository.GetByIdAsync(id);

        if (user == null || user.Role == UserRole.Admin)
            return null;

        user.BlockingDate = DateTime.Now;
        userRepository.Update(user);
        await uow.SaveChangesAsync();
        return user;
    }


    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user == null|| user.Role == UserRole.Admin)
            return false;

        userRepository.Remove(user);
        await uow.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ChangeRoleAsync(int id, UserRole newRole)
    {
        var user = await userRepository.GetByIdAsync(id);
        if(user == null || user.Role == UserRole.Admin)
            return false;

        user.Role = newRole;
        userRepository.Update(user);
        await uow.SaveChangesAsync();
        return true;
    }

    public async Task ChangePasswordAsync(int id, string newPassword)
    {
        var user = await userRepository.GetByIdAsync(id);
        user.Password = newPassword;
        userRepository.Update(user);
        await uow.SaveChangesAsync();
    }

}
