using System.Runtime.Serialization;

namespace CRM_DOBRO.Enums
{
    public enum ContactStatus
    {
        [EnumMember(Value = "Cold")]
        Cold = 1,

        [EnumMember(Value = "Warm")]
        Warm,

        [EnumMember(Value = "Lead")]
        Lead
    }
}
