using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iesi=Iesi.Collections.Generic;

namespace Invoices.Domain.Model.Client
{
    public class Client
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
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
                                    String.Format("Dane:{0}{1}", Environment.NewLine, Name + " " + Surname) +
                                    "====================================" +
                                    String.Format("Adres:{0}{0}{1}", Environment.NewLine, Localisation.FormatString()) +
                                    "====================================" +
                                    String.Format("Kontakty:{0}", Environment.NewLine);
            foreach (Contact a in ListOfContact)
                text += a.FormatString() + "------------------------------------\n";
                              text+="====================================" +
                                    String.Format("Zniżki:{0}", Environment.NewLine);
            foreach (Discount a in ListOfDiscount)
                text += a.FormatString() + "------------------------------------";
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
    }
}
