namespace CRM_DOBRO.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public required int LeadId { get; set; }
        public int SalerId { get; set;}
        public required DateTime DateOfSale { get; set; }

        // Навигационные свойства

        public Lead? Lead { get; set; }
        public User? Saler { get; set; }
    }

}
/*
 * Продажа (Sale):
- Идентификатор (обяз)
- Идентификатор лида (обяз) [один ко многим]
- Идентификатор продажника (обяз) [один ко многим]
- Дата продажи / заключения договора (обяз)
*/
