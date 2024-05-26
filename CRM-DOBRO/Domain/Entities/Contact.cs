using Domain.Abstractions;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Contact : Entity
{
    public required int MarketingId { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; }
    [MaxLength(50)]
    public string? Surname { get; set; }
    [MaxLength(50)]
    public string? LastName { get; set; }
    [MaxLength(15)]
    public required string PhoneNumber { get; set; }
    [MaxLength(20)]
    public string? Email { get; set; }
    public required ContactStatus Status { get; set; }
    public required DateTime DateOfLastChanges { get; set; }

    // Навигационные свойства
    public User? Marketing { get; set; }
    public Lead? Lead { get; set; }
}

