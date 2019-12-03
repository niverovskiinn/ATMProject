using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.Models
{
    internal class Transaction : IEntity
    {
        public int Id { get; set; }

        public DateTime DateT { get; set; }

        public TransactionType Type
        {
            get;
            set;
        }

        public decimal AmountMoney { get; set; }

        public int AccountFromId { get; set; }
        
        public int? AccountToId { get; set; }
        
        public string Notes { get; set; }
    }
}
