using CRM_DOBRO.Data;
using CRM_DOBRO.Entities;
using System.Runtime.CompilerServices;

namespace CRM_DOBRO.Services
{
    public class AdminService
    {
        private readonly CRMDBContext _context;
        public AdminService(CRMDBContext context)
        {
            _context = context;
        }

        public async Task NewAdmin()
        {
            var Admin =  new User
            {
                Email = "alonastq@gmail.com",
                FullName = "Mamedov Nizar",
                Password = "babaduck",
                Role = Enums.UserRole.Admin,
            };
            _context.Users.Add(Admin);
            await _context.SaveChangesAsync();
        }
    }
}
