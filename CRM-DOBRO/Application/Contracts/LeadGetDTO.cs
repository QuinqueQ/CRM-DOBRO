﻿namespace Application.Contracts;

/// <summary>
/// DTO for obtaining lead fields
/// </summary>
public class LeadGetDTO
{
    public int Id { get; set; }
    public required int ContactId { get; set; }

    [JsonProperty("Contact")]
    public required string ContactFullName { get; set; }
    public int? SalerId { get; set; }

    [JsonProperty("Saler")]
    public required string SalertFullName { get; set; }
    public LeadStatus Status { get; set; }

}
