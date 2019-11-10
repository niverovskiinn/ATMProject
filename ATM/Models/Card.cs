using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Card : IEntity
    {
        [Key]
        [StringLength(16, MinimumLength = 16)]
        public string Number { get; set; }

        public DateTime ExpireDate { get; set; }

        public string Cvv2 { get; set; }

        public string PinHash { get; set; }


        public int AccountId { get; set; }

        public Account Account { get; set; }

        public string Notes { get; set; }
    }
}