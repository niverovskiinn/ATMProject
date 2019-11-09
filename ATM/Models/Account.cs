using System;

namespace Models
{
    public class Account: IEntity
    {
        public int Id { get; set; }
        
        public AccountType Type { get; set; }
        
        public decimal AmountMoney { get; set; }
        
        public DateTime Creation { get; set; }
        
        public AccountStatus Status { get; set; }
        
        public string Notes { get; set; }

        public User Owner { get; set; }
    }
}