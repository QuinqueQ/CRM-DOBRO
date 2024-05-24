using System.Runtime.Serialization;

namespace Domain.Enums
{
    /// <summary>
    /// Enum for user roles
    /// </summary>
    public enum UserRole
    {
        [EnumMember(Value = "Admin")]
        Admin = 1,

        [EnumMember(Value = "Marketing")]
        Marketing,

        [EnumMember(Value = "Saler")]
        Saler
    }
}
