﻿using System;
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
        public int Id { get; set; }

        public int Type { get; set; }

        public decimal AmountMoney { get; set; }

        public DateTime Creation { get; set; }

        public int Status { get; set; }

        public string OwnerPassport { get; set; }

        public ICollection<Card> Cards { get; set; }

        public string Notes { get; set; }
    }
}
