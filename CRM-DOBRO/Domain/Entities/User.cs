using Domain.Abstractions;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User : Entity
{
    [MaxLength(150)]
    public required string FullName { get; set; }
    [MaxLength(20)]
    public required string Email { get; set; }
    [MaxLength(30)]
    public required string Password { get; set; }
    public required UserRole Role { get; set; }
    public DateTime? BlockingDate { get; set; }

    // Навигационные свойства
    public List<Contact> Contacts { get; set; } = [];
    public List<Lead> Leads { get; set; } = [];
    public List<Sale> Sales { get; set; } = [];
}




