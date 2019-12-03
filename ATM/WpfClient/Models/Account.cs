using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// ENUM: Account types:
/// 0 : Debit
/// 1 : Credit
///
/// ENUM: Account statuses:
/// 0 : Closed
/// 1 : Active
/// 2 : Frozen
///
///
/// ENUM: Transaction types:
/// 0 : Withdraw
/// 1 : ToUser
/// 2 : Deposit //cash into ATM
/// </summary>


namespace WpfClient.Models
{
    public class Account : IEntity
    {
        #region Properties
        public int Id { get; set; }

        public int TypeId { get; set; }

        public decimal AmountMoney { get; set; }

        public DateTime Creation { get; set; }

        public int StatusId { get; set; }

        public string OwnerPassport { get; set; }

        public string Notes { get; set; }
        #endregion


        public Account(int id, int typeId, decimal amountMoney, DateTime creation, int statusId, string ownerPassport, string notes)
        {
            Id = id;
            TypeId = typeId;
            AmountMoney = amountMoney;
            Creation = creation;
            StatusId = statusId;
            OwnerPassport = ownerPassport;
            Notes = notes;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }


}
