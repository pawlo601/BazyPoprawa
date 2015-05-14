using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
/*
drop table LOCALISATION
go

CREATE TABLE [dbo].[LOCALISATION](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[street] [varchar](50) NOT NULL,
    [nr] [varchar](10) NOT NULL,
    [city] [varchar](50) NOT NULL,
    [code] [varchar](50) NOT NULL,
    [country] [varchar](50) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
 */
namespace Invoices.Domain.Model.Client
{
    [HasSelfValidation]
    public class Address
    {
        public virtual int ID { get; set; }
        [StringLengthValidator(5, 25, MessageTemplate = "Nazwa ulicy powinna mieć od 5 do 30 znaków")]
        [RegexValidator("[A-Z]{1}[a-z]{4-24}", MessageTemplate = "Nazwa ulicy powinna zawierć same litery")]
        public virtual string Street { get; set; }
        [StringLengthValidator(0, 10, MessageTemplate = "Numer budynku oraz mieszkania nie powinien przkraczać 10 znaków")]
        public virtual string NumberOfBuilding { get; set; }
        [StringLengthValidator(5, 15, MessageTemplate = "Nazwa miejscowości powinna mieć od 5 do 15 znaków")]
        [RegexValidator("[A-Z]{1}[a-z]{4-14}", MessageTemplate = "Nazwa miejscowości powinna zawierć same litery")]
        public virtual string City { get; set; }
        [StringLengthValidator(6, 6, MessageTemplate = "Kod pocztowy powienien zawierać 6 znaków")]
        [RegexValidator("[0-9]{2}-[0-9]{3}", MessageTemplate = "Kod pocztowy powien wyglądać: XX-XXX i zawierać same cyfry")]
        public virtual string Code { get; set; }
        [StringLengthValidator(5, 15, MessageTemplate = "Nazwa państwa powinna mieć od 5 do 15 znaków")]
        [RegexValidator("[A-Z]{1}[a-z]{4-14}", MessageTemplate = "Nazwa państwa powinna zawierć same litery")]
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
            return String.Format("{0}Ulica: {1}{0}Nr: {2}{0}Miejscowość: {3} {4}{0}Państwo: {5}",
                                Environment.NewLine,
                                Street,
                                NumberOfBuilding,
                                City,
                                Code,
                                Country);
        }
        public override string ToString()
        {
            return Street + " " + NumberOfBuilding + "\n" +
                   City + " " + Code + "\n" +
                   Country;
        }
    }
}
