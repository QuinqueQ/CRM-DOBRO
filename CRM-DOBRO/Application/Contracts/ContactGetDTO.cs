using Domain.Enums;
using Newtonsoft.Json;

namespace Application.Contracts
{
    /// <summary>
    /// DTO for obtaining contact fields
    /// </summary>
    public class ContactGetDTO
    {
        public int Id { get; set; }
        public required int MarketingId { get; set; }
        [JsonProperty("Marketing")]
        public required string MarketingFullName { get; set; }
        public required string Name { get; set; }
        public string? Surname { get; set; }
        public string? LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public required ContactStatus Status { get; set; }
        public required DateTime DateOfLastChanges { get; set; }
    }
}
