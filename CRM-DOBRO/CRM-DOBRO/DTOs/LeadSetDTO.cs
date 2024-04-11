using CRM_DOBRO.Enums;
using CRM_DOBRO.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM_DOBRO.DTOs
{/// <summary>
/// DTO for creating and changing a lead
/// </summary>
    public class LeadSetDTO
    {
        [DisplayName("ID Контакта")]
        [Required(ErrorMessage = Message.REQUIRED)]
        public required int ContactId { get; init; }

        [DisplayName("Статус лида")]
        [Required(ErrorMessage = Message.REQUIRED)]
        [EnumDataType(typeof(LeadStatus))]
        public LeadStatus Status { get; init; }
    }
}
