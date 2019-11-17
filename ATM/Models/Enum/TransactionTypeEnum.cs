using System.ComponentModel;

namespace Models.Enum
{
    public enum TransactionTypeEnum
    {
        [Description("Withdraw cash from ATM")]
        Withdraw = 0,

        [Description("Send money to another person")]
        ToUser,
        
        [Description("Deposit money to account")]
        Deposit
    }
}