using System.Runtime.Serialization;

namespace Domain.Enums;

/// <summary>
/// Enum for contact statuses
/// </summary>
public enum ContactStatus
{
    [EnumMember(Value = "Cold")]
    Cold = 1,

    [EnumMember(Value = "Warm")]
    Warm,

    [EnumMember(Value = "Lead")]
    Lead
}
