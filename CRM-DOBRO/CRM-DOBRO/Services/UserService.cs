using CRM_DOBRO.Data;
using CRM_DOBRO.DTOs;
using CRM_DOBRO.Entities;
using CRM_DOBRO.Enums;
using Microsoft.EntityFrameworkCore;

namespace CRM_DOBRO.Services
{
    public class UserService(CRMDBContext context)
    {
        private readonly CRMDBContext _context = context;

        public async Task NewAdmin()
        {
            var Admin =  new User
            {
                Email = "alonastq@gmail.com",
                FullName = "Mamedov Nizar",
                Password = "228615",
                Role = Enums.UserRole.Admin,
            };
            _context.Users.Add(Admin);
            await _context.SaveChangesAsync();
        }

        public async Task CreateNewUserAsync(UserSetDTO newuser)
        {
            User user = new()
            {
                FullName = newuser.FullName,
                Password = newuser.Password,
                Role = newuser.Role,
                Email = newuser.Email,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserGetDTO>> GetAllUsersAsync()
        {
            List<User> users = await _context.Users.ToListAsync();
            List<UserGetDTO> usersDTO = [];

            foreach (var user in users)
            {
                UserGetDTO userDTO = new ()
                {
                    FullName = user.FullName,
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role,
                    BlockingDate = user.BlockingDate,
                };
                usersDTO.Add(userDTO);
            }
            return usersDTO;
        }

        public async Task<User?> LogInUserAsync(string email, string password)
        {

            var selectedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return selectedUser;
        }

        public async Task<User?> BanUserAsync(int id)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null || user.Role == UserRole.Admin)
                return null;

            user.BlockingDate = DateTime.Now;
            _context.Update(user);
            _context.SaveChanges();

            return user;
        }


        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == id);
            if (user == null|| user.Role == UserRole.Admin)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeRoleAsync(int id, UserRole newRole)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == id);
            if(user == null || user.Role == UserRole.Admin)
                return false;

            user.Role = newRole;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task ChangePasswordAsync(int id, string newPassword)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == id);
            user.Password = newPassword;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

    }
}
