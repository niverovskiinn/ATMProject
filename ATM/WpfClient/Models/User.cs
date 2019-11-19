using System;
using System.Collections.Generic;

namespace WpfClient.Models
{
    public class User
    {
        public string Passport { get; set; }

        public string FullName { get; set; }

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
        
        public override string ToString()
        {
            return $"User: {FullName}";
        }
    }
}
