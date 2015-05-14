using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iesi=Iesi.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
/*
drop table CLIENT
go

CREATE TABLE [dbo].[CLIENT](
	[id] [int] IDENTITY(1,2) NOT NULL,
	[name] [varchar](50) NULL,
    [surname] [varchar](50) NULL,
    [address] [int] NOT NULL
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
    public class Client
    {
        public virtual int ID { get; set; }
        [StringLengthValidator(3, 10, MessageTemplate = "Imie powinno mieć od 3 do 10 znaków")]
        [RegexValidator("[A-Z]{1}[a-z]{2-9}", MessageTemplate = "Tylko litery")]
        public virtual string Name { get; set; }
        [StringLengthValidator(3, 20, MessageTemplate = "Imie powinno mieć od 3 do 20 znaków")]
        [RegexValidator("[A-Z]{1}[a-z]{2-19}", MessageTemplate = "Tylko litery")]
        public virtual string Surname { get; set; }
        public virtual Address Localisation { get; set; }
        public virtual iesi.ISet<Contact> ListOfContact { get; set; }
        public virtual iesi.ISet<Discount> ListOfDiscount { get; set; }

        public Client()
        {
            ID = -1;
            Random rand = new Random();
            Name = "Imie" + rand.Next(1, 2000000).ToString();
            Surname = "Nazwisko" + rand.Next(1, 2000000).ToString();
            Localisation = new Address();
            ListOfContact = new iesi.HashedSet<Contact>();
            ListOfDiscount = new iesi.HashedSet<Discount>();
        }
        public Client(string name, string surname, Address loc)
        {
            ID = -1;
            this.Name = name;
            this.Surname = surname;
            this.Localisation = loc;
            ListOfContact = new iesi.HashedSet<Contact>();
            ListOfDiscount = new iesi.HashedSet<Discount>();
        }
        public virtual void AddSomeDiscounts()
        {
            Random rand = new Random();
            int j = rand.Next(1, 6);
            for (int i = 0; i < j; i++)
            {
                ListOfDiscount.Add(new Discount());
                System.Threading.Thread.Sleep(50);
            }
        }
        public virtual void AddSomeContacts()
        {
            Random rand = new Random();
            int j = rand.Next(1, 6);
            for (int i = 0; i < j; i++)
            {
                ListOfContact.Add(new Contact());
                System.Threading.Thread.Sleep(50);
            }
        }
        public virtual void AddDiscount(Discount dis)
        {
            ListOfDiscount.Add(dis);
        }
        public virtual void AddContact(Contact contact)
        {
            ListOfContact.Add(contact);
        }
        public virtual string FormatString()
        {
            string text = String.Format("ID klienta: {1}{0}", Environment.NewLine, ID.ToString()) +
                                    "====================================" +
                                    String.Format("{0}Dane:{0}{0}{1}{0}", Environment.NewLine, Name + " " + Surname) +
                                    "====================================" +
                                    String.Format("{0}Adres:{0}{1}{0}", Environment.NewLine, Localisation.FormatString()) +
                                    "====================================" +
                                    String.Format("{0}Kontakty:{0}", Environment.NewLine);
            try
            {
                foreach (Contact a in ListOfContact)
                    text += String.Format("{0}------------------------------------{0}{1}",
                                           Environment.NewLine,
                                           a.FormatString());
            }
            catch(Exception)
            {

            }
            text += String.Format("{0}===================================={0}",
                    Environment.NewLine) +
                                    String.Format("Zniżki:{0}{0}", Environment.NewLine);
            try
            {
                foreach (Discount a in ListOfDiscount)
                    text += String.Format("------------------------------------{0}{1}{0}", Environment.NewLine, a.FormatString());
            }
            catch(Exception)
            {

            }
            text += String.Format("===================================={0}",
                    Environment.NewLine);
            return text;
        }
        public override string ToString()
        {
            string text = String.Format("{1}{0}{2}{0}{3}{0}{4}",
                            Environment.NewLine,
                            ID.ToString(),
                            Name,
                            Surname,
                            Localisation.ToString());
            foreach (Contact a in ListOfContact)
                text += a.ToString() + "\n";
            foreach (Discount a in ListOfDiscount)
                text += a.ToString() + "\n";
            return text;
        }
        [SelfValidation]
        public virtual void Validation(ValidationResults results)
        {
            if (Localisation == null ||
                ListOfContact == null ||
                ListOfDiscount == null)
                results.AddResult(new ValidationResult("Lokalizacja, lista kontaktów oraz zniżek nie powinna być null", 
                                                        this, "Validation", string.Empty, null));
        }
    }
}
