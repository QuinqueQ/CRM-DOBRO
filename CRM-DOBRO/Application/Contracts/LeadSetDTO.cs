namespace Application.Contracts;
/// <summary>
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
