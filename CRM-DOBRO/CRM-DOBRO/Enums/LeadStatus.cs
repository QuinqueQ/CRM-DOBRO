using System.Runtime.Serialization;

namespace CRM_DOBRO.Enums
{
    /// <summary>
    /// Enum for lead statuses
    /// </summary>
    public enum  LeadStatus
    {
        [EnumMember(Value = "New")]
        New = 1,

        [EnumMember(Value = "Proposition")]
        Proposition,

        [EnumMember(Value = "Negotiation")]
        Negotiation,

        [EnumMember(Value = "Contract")]
        Contract,

        [EnumMember(Value = "Lost")]
        Lost
    }
}
