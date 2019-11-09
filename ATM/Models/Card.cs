using System;

namespace Models
{
    public class Card : IEntity
    {
        public string Number { get; set; }

        public DateTime ExpireDate { get; set; }

        public string Cvv2 { get; set; }

        public string PinHash { get; set; }
        
        public Account Belonging { get; set; }
        
        public string Notes { get; set; }

        
    }
}