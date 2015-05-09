using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
/*
drop table CONTACTS
go

CREATE TABLE [dbo].[CONTACTS](
	[ID_Client] [int] NOT NULL,
	[contact] [varchar](50) NULL
)
GO

SET ANSI_PADDING OFF
GO
 */
namespace Invoices.Domain.Model.Client
{
    public class Contact
    {
        public virtual string ContactTo { get; set; }
        public Contact()
        {
            Random rand = new Random();
            int a = rand.Next(0, 2);
            if (a == 0)
                ContactTo = "123456789";
            else
                ContactTo = "Przykładowy@Adres.com";
        }
        public Contact(string prefix, string number)
        {
            if (number.Length == 9)
            {
                int[] numbers = new int[number.Length];
                for (int c = 0; c < number.Length; c++)
                    if (!int.TryParse(number.Substring(c, 1), out numbers[c]))
                        throw new Exception("Błąd w numerze.Niewłaściwe znaki.\n");
                ContactTo = prefix + number;
            }
            else
                throw new Exception("Zła długość numeru.\n");
        }
        public Contact(string mail)
        {
            try
            {
                new MailAddress(mail);
                ContactTo = mail;
            }
            catch(Exception e)
            {
                throw new Exception("Błąd w adresie\n" + e.Message);
            }
        }
        public string FormatString()
        {
            return "Kontakt: " + ContactTo;
        }
        public override string ToString()
        {
            return ContactTo;
        }
    }
}
