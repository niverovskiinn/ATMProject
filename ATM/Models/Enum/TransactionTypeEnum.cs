using System.ComponentModel;

namespace Models.Enum
{
    public enum TransactionTypeEnum
    {
        [Description("Withdraw cash from ATM")]
        Withdraw,

        [Description("Send money to another person")]
        ToUser,
        
        [Description("Deposit money to account")]
        Deposit
    }
}