using CRM_DOBRO.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_DOBRO.Entities
{
    public class Lead
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int Id { get; set; }
        [ForeignKey("Contact")]
        public required int ContactId { get; set; }
        [ForeignKey("Saler")]
        public int? SalerId { get; set; }
        public LeadStatus Status { get; set; }

        // Навигационные свойства
        public required Contact Contact { get; set; }
        public User? Saler { get; set; }
        public List<Sale>? Sales { get; set; }
    }
    
}
/*
 * Лид (Lead):
- Идентификатор (обяз)
- Идентификатор контакта (обяз) [один к одному]
- Идентификатор продажника (не обяз) [один ко многим]
- Статус лида (обяз) [enum - New, Proposition, Negotiation, Contract, Lost]
*/
