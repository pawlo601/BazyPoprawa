using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Domain.Model.Client;

namespace Finance.ObjectMothers
{
    public class ClientObjectMothers
    {
        public static Client CreateClientPrivateWithoutDiscount()
        {
            Address addr = new Address();
            Client a = new Client("Imie1","Nazwisko1", addr);
            return a;
        }
        public static Client CreateClientPrivateWithDiscountNetto()
        {
            Address addr = new Address();
            Client a = new Client("Imie2", "Nazwisko2", addr);
            Discount dis = new Discount(1, Bonus.Netto);
            a.AddDiscount(dis);
            return a;
        }
        public static Client CreateClientPrivateWithDiscountZnizka()
        {
            Address addr = new Address();
            Client a = new Client("Imie3", "Nazwisko3", addr);
            Discount dis = new Discount(1, Bonus.Zniżka, 0.10f);
            a.AddDiscount(dis);
            return a;
        }
        public static Company CreateClientCompanyWithoutDiscount()
        {
            Address addr = new Address();
            Regon reg = new Regon();
            NIP nip = new NIP();
            Company a = new Company("Firma1", addr, nip, reg);
            return a;
        }
        public static Client CreateClientCompanyWithDiscountNetto()
        {
            Address addr = new Address();
            Regon reg = new Regon();
            NIP nip = new NIP();
            Company a = new Company("Firma2", addr, nip, reg);
            Discount dis = new Discount(1, Bonus.Netto);
            a.AddDiscount(dis);
            return a;
        }
        public static Client CreateClientCompanyWithDiscountZnizka()
        {
            Address addr = new Address();
            Regon reg = new Regon();
            NIP nip = new NIP();
            Company a = new Company("Firma3", addr, nip, reg);
            Discount dis = new Discount(1, Bonus.Zniżka, 0.1f);
            a.AddDiscount(dis);
            return a;
        }
        public static Address CreateAddress()
        {
            Address addr = new Address("Ulica", "5/6", "Wrocław", "12-345", "Poland");
            return addr;
        }
        public static Discount CreateDiscountNetto()
        {
            Discount dis = new Discount(1, Bonus.Netto);
            return dis;
        }
        public static Discount CreateDiscountZnizka()
        {
            Discount dis = new Discount(1, Bonus.Zniżka);
            return dis;
        }
        public static Regon CreateRegon()
        {
            return new Regon();
        }
        public static NIP CreateNip()
        {
            return new NIP();
        }
        public static Contact CreatePhone()
        {
            return new Contact("+48", "123456789");
        }
        public static Contact CreateMail()
        {
            return new Contact("przyklad@maila.com");
        }
    }
}
