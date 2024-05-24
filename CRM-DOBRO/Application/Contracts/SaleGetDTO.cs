using Newtonsoft.Json;

namespace Application.Contracts
{
    /// <summary>
    /// DTO for obtaining sale fields
    /// </summary>
    public class SaleGetDTO
    {
        public int Id { get; set; }
        public required int LeadId { get; set; }

        [JsonProperty("Lead")]
        public required string LeadtFullName { get; set; }
        public int SalerId { get; set; }

        [JsonProperty("Saler")]
        public string? SalerFullName { get; set; }
        public required DateTime DateOfSale { get; set; }


    }
}
