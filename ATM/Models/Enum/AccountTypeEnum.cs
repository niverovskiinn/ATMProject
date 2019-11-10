using System.ComponentModel;

namespace Models.Enum
{
    public enum AccountTypeEnum
    {
        [Description("Debit account")] Debit,
        [Description("Credit account")] Credit
    }
}