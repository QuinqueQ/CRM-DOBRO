using CRM_DOBRO.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM_DOBRO.DTOs
{
    public class SaleSetDTO
    {
        [DisplayName("ID Лида")]
        [Required(ErrorMessage = Message.REQUIRED)]
        public required int LeadId { get; init; }
        [DisplayName("Дата прожажи")]
        [Required(ErrorMessage = Message.REQUIRED)]
        public required DateTime DateOfSale { get; init; }
    }
}
