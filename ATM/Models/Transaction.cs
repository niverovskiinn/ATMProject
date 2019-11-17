using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Enum;

namespace Models
{
    public class Transaction : IEntity
    {
        [Key] public int Id { get; set; }

        public DateTime DateTimeTr { get; set; }

        public virtual TransactionType Type { get; set; }

        public decimal AmountMoney { get; set; }

        public int AccountFromId { get; set; }
        public int? AccountToId { get; set; }
        
        public string Notes { get; set; }
    }
}