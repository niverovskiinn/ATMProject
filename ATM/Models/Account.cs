using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Enum;

namespace Models
{
    public class Account : IEntity
    {
        [Key] public int Id { get; set; }

        public virtual AccountType Type { get; set; }

        public decimal AmountMoney { get; set; }

        public DateTime Creation { get; set; }

        public virtual AccountStatus Status { get; set; }


//        public string OwnerPassport { get; set; }

        public User Owner { get; set; }
        public ICollection<Card> Cards { get; set; }

        public string Notes { get; set; }
    }
}