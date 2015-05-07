using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Domain.Model.Client
{
    public class Address
    {
        public virtual int ID { get; set; }
        public virtual string Street { get; set; }
        public virtual string NumberOfBuilding { get; set; }
        public virtual string City { get; set; }
        public virtual string Code { get; set; }
        public virtual string Country { get; set; }

        public Address()
        {
            ID = -1;
            Random ran = new Random();
            Street = "Ulica" + ran.Next(0, 2000000).ToString();
            NumberOfBuilding = "Nr " + ran.Next(0, 2000000).ToString();
            City = "City" + ran.Next(0, 2000000).ToString();
            Code = ran.Next(10, 99).ToString() + "-" + ran.Next(100, 999).ToString();
            Country = "Panstwo" + ran.Next(0, 2000000).ToString();
        }
        public Address(String street, String number,
                String city, String code,
                String country)
        {
            Street = street;
            NumberOfBuilding = number;
            City = city;
            Code = code;
            Country = country;
        }
        public virtual string FormatString()
        {
            return "Ulica: " + Street + " " + NumberOfBuilding + "\n" +
                    "Miejscowość: " + City + " " + Code + "\n" +
                    "Państwo: " + Country + "\n";
        }
        public override string ToString()
        {
            return Street + " " + NumberOfBuilding + "\n" +
                   City + " " + Code + "\n" +
                   Country;
        }
    }
}
