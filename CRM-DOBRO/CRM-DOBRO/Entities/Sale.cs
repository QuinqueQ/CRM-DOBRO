using CRM_DOBRO.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_DOBRO.Entities
{
    public class Sale
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Lead")]
        public required int LeadId { get; set; }
        [ForeignKey("Saler")]
        public int SalerId { get; set;}
        public required DateTime DateOfSale { get; set; }

        // Навигационные свойства
        public required Lead Lead { get; set; }
        public required User Saler { get; set; }
    }

}
/*
 * Продажа (Sale):
- Идентификатор (обяз)
- Идентификатор лида (обяз) [один ко многим]
- Идентификатор продажника (обяз) [один ко многим]
- Дата продажи / заключения договора (обяз)
*/
