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

        public DateTime DateTime { get; set; }

        public int Type { get; set; }

        public decimal AmountMoney { get; set; }

        public int AccountFromId { get; set; }

        public Account From { get; set; }

        public int? AccountToId { get; set; }

        public Account To { get; set; }

        public string Notes { get; set; }
    }
}
