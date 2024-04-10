using Newtonsoft.Json;

namespace CRM_DOBRO.DTOs
{
    public class SaleGetDTO
    {
        public int Id { get; set; }
        public required int LeadId { get; set; }
        public int SalerId { get; set; }
        public required DateTime DateOfSale { get; set; }

        [JsonProperty("Lead")]
        public required string LeadtFullName { get; set; }

        [JsonProperty("Saler")]
        public string? SalerFullName { get; set; }
    }
}
