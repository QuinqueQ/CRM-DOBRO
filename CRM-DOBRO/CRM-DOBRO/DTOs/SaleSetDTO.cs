﻿namespace CRM_DOBRO.DTOs
{
    public class SaleSetDTO
    {
        public required int LeadId { get; set; }
        public int SalerId { get; set; }
        public required DateTime DateOfSale { get; set; }
    }
}
