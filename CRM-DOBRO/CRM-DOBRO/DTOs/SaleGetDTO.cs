namespace CRM_DOBRO.DTOs
{
    public class SaleGetDTO
    {
        public int Id { get; set; }
        public required int LeadId { get; set; }
        public int SalerId { get; set; }
        public required DateTime DateOfSale { get; set; }
    }
}
