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

        public int  TypeId { get; set; }

        public decimal AmountMoney { get; set; }

        public DateTime Creation { get; set; }

        public int StatusId { get; set; }
        
        public string OwnerPassport { get; set; }
        
        public string Notes { get; set; }
    }
}