using System.ComponentModel;

namespace Models.Enum
{
    public enum AccountTypeEnum
    {
        [Description("Debit account")] Debit = 0,
        [Description("Credit account")] Credit
    }
}