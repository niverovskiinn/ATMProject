using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WpfClient.Models
{
    internal class Transaction : IEntity
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("typeId")]
        public TransactionType Type
        {
            get;
            set;
        }

        [JsonProperty("amountMoney")]
        public decimal AmountMoney { get; set; }

        [JsonProperty("accountFromId")]
        public int AccountFromId { get; set; }

        [JsonProperty("accountToId")]
        public int? AccountToId { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        public Transaction(int id, DateTime dateT, TransactionType type, decimal amountMoney, int accountFromId, int? accountToId, string notes)
        {
            Id = id;
            DateTime = dateT;
            Type = type;
            AmountMoney = amountMoney;
            AccountFromId = accountFromId;
            AccountToId = accountToId;
            Notes = notes;
        }
    }
}
