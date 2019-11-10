using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class User: IEntity
    {
        [Key,StringLength(8, MinimumLength = 8)]
        public string Passport { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName} {Patronymic}";
        
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        public DateTime DateBirth { get; set; }

        public string TaxNumber { get; set; }
        
        public string Telephone1 { get; set; }
        
        public string Telephone2 { get; set; }

        public string Telephone3 { get; set; }
        
        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNum { get; set; }

        public string ApartmentNum { get; set; }
        
        public string Notes { get; set; }
        
        
        public ICollection<Account> Accounts { get; set; }

    }
}