using CRM_DOBRO.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_DOBRO.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required UserRole Role { get; set; }
        public DateTime? BlockingDate { get; set; }

    }
    
}
/*
 Пользователь (User):
-Идентификатор(обяз)
- ФИО(не обяз)
- Адрес электронной почты(обяз)
-Пароль(обяз)
- Роль(обяз)[enum -Admin, Marketing, Sales]
- Дата блокировки (не обяз)
*/



