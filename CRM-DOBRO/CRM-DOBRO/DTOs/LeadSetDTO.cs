using CRM_DOBRO.Enums;

namespace CRM_DOBRO.DTOs
{
    public class LeadSetDTO
    {
        public required int ContactId { get; set; }
        public LeadStatus Status { get; set; }
    }
}
