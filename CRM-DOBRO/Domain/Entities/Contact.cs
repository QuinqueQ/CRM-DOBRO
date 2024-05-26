﻿using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entities
{
    public class Contact : Entity
    {
        public required int MarketingId { get; set; }
        public required string Name { get; set; }
        public string? Surname { get; set; }
        public string? LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public required ContactStatus Status { get; set; }
        public required DateTime DateOfLastChanges { get; set; }

        // Навигационные свойства
        public User? Marketing { get; set; }
        public Lead? Lead { get; set; }
    }
   
}
