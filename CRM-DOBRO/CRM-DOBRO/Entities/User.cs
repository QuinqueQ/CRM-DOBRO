using CRM_DOBRO.Enums;

namespace CRM_DOBRO.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required UserRole Role { get; set; }
        public DateTime? BlockingDate { get; set; }

        // Навигационные свойства
        public List<Contact>? Contacts { get; set; }
        public List<Lead>? Leads { get; set; }
        public List<Sale>? Sales { get; set; }
    }
}




