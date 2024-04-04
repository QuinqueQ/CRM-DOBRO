using CRM_DOBRO.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_DOBRO.Entities
{
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Marketing")]
        public required int MarketingId { get; set; }
        public required string Name { get; set; }
        public string? Surname { get; set; }
        public string? LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public required ContactStatus Status { get; set; }
        public required DateTime DateOfLastChanges { get; set; }

        // Навигационные свойства
        public required User Marketing { get; set; }
    }
   
}
/*
 * Контакт (Contact):
- Идентификатор (обяз)
- Идентификатор маркетолога (обяз) [один ко многим]
- Имя (обяз)
- Фамилия (не обяз)
- Отчество (не обяз)
- Номер телефона (обяз)
- Адрес электронной почты (не обяз)
- Статус контакта (обяз) [enum - Cold, Warm, Lead]
- Дата последнего изменения контакта (обяз)
*/ 