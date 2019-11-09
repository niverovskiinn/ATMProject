using System;

namespace Models
{
    
    public class Transaction: IEntity
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public TransactionType Type { get; set; }

        public decimal AmountMoney { get; set; }

        public string Notes { get; set; }
        
        public Account From { get; set; }

        public Account To { get; set; }
    }
}