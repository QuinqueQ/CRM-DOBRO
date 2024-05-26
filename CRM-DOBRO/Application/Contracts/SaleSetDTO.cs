namespace Application.Contracts;

/// <summary>
/// DTO for creating and changing a sale
/// </summary>
public class SaleSetDTO
{
    [DisplayName("ID Лида")]
    [Required(ErrorMessage = Message.REQUIRED)]
    public required int LeadId { get; init; }
    [DisplayName("Дата прожажи")]
    [Required(ErrorMessage = Message.REQUIRED)]
    public required DateTime DateOfSale { get; init; }
}
