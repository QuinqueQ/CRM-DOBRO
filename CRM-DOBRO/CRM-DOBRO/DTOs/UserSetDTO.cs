using CRM_DOBRO.Enums;

namespace CRM_DOBRO.DTOs
{
    public class UserSetDTO
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required UserRole Role { get; set; }
    }
}
