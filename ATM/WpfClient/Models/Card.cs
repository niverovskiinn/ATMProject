using System;

namespace WpfClient.Models
{
    public class Card : IEntity
    {
        public string Number { get; set; }

        public DateTime ExpireDate { get; set; }

        public string Cvv2 { get; set; }

        public string PinHash { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }

        public string Notes { get; set; }
    }
}
