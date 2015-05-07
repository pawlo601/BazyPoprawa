using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Domain.Model.Invoice;
using Invoices.Domain.Model.Client;
using Invoices.Domain.Model.Product;
using Invoices.Domain.Model.Invoice.Repositories;
using collections = System.Collections.Generic;

namespace Finance.Infrastructure.Repositories
{
    public class InvoiceIM:IInvoiceRepositories
    {
        private collections.List<Invoice> invoices = new collections.List<Invoice>();
        public InvoiceIM()
        {
            Address ad1 = new Address("S1", "12", "Wrocław", "12-456", "Poland");
            Regon a1 = new Regon("2346789");
            NIP a2 = new NIP("123123");
            Company b = new Company("Firma", ad1, a2, a1);
            Price c3 = new Price(10, Waluta.PLN, 19.0f);
            Product a3 = new Product("Pierwsza rzecz", TypProduktu.Usługa, c3);
            Money a4 = new Money(123, Waluta.PLN);
            Item e = new Item(a3, 12);
            collections.List<Item> d = new collections.List<Item>();
            d.Add(e);
            Invoice a = new Invoice("Faktura", b);
            invoices.Add(a);
        }
        public void Insert(Invoice invoice)
        {
            invoices.Add(invoice);
        }
        public void Delete(string Id)
        {
            foreach (var a in invoices)
            {
                if (Id == a.ID)
                {
                    invoices.Remove(a);
                }
            }
        }
        public Invoice Find(string Id)
        {
            foreach (var a in invoices)
            {
                if (Id == a.ID)
                {
                    return a;
                }
            }
            return null;
        }
        public collections.List<Invoice> FindAll()
        {
            return invoices;
        }
        public collections.List<Invoice> FindAllPerContractor(int idOfContractor)
        {
            collections.List<Invoice> lista = new collections.List<Invoice>();
            foreach (var a in invoices)
            {
                if (idOfContractor == a.Contractor.ID)
                {
                    lista.Add(a);
                }
            }
            return lista;
        }
        public collections.List<Invoice> FindAllPerData(DateTime date)
        {
            collections.List<Invoice> lista = new collections.List<Invoice>();
            foreach (var a in invoices)
            {
                if (date == a.DateOfCreate)
                {
                    lista.Add(a);
                }
            }
            return lista;
        }
    }
}
