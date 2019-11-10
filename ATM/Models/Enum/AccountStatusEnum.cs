using System.ComponentModel;

namespace Models.Enum
{
    public enum AccountStatusEnum
    {
        [Description("Closed account")]
        Closed,
        [Description("Active account")]
        Active,
        [Description("Frozen account")]
        Frozen
    }
}