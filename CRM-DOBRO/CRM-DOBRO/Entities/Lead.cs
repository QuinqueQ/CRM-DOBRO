using CRM_DOBRO.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_DOBRO.Entities
{
    public class Lead
    {
        public  int Id { get; set; }
        public required int ContactId { get; set; }
        public int? SalerId { get; set; }
        public LeadStatus Status { get; set; }

        // Навигационные свойства

        public Contact? Contact { get; set; }
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
