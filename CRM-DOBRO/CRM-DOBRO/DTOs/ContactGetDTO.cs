using CRM_DOBRO.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM_DOBRO.DTOs
{
    public class ContactGetDTO
    {
        public int Id { get; set; }
        public required int MarketingId { get; set; }
        public required string Name { get; set; }
        public string? Surname { get; set; }
        public string? LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public required ContactStatus Status { get; set; }
        public required DateTime DateOfLastChanges { get; set; }
    }
}
