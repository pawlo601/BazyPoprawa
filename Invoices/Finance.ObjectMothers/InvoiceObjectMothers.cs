using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Domain.Model.Invoice;

namespace Finance.ObjectMothers
{
    public class InvoiceObjectMothers
    {
        public static Invoice CreateInvoicePrivNoDis1PrzedPLN()
        {
            Invoice a = new Invoice("Faktura zapłaty", ClientObjectMothers.CreateClientPrivateWithoutDiscount());
            a.AddProduct(ProductObjectMothers.CreateProductPrzedmiotPLN(), 1);
            return a;
        }
        public static Invoice CreateInvoicePrivWithDis2PrzedEUR()
        {
            Invoice a = new Invoice("Faktura zapłaty2", ClientObjectMothers.CreateClientPrivateWithDiscountNetto());
            a.AddProduct(ProductObjectMothers.CreateProductPrzedmiotEUR(), 1);
            a.AddProduct(ProductObjectMothers.CreateProductPrzedmiotEUR(), 2);
            return a;
        }
        public static Invoice CreateInvoiceCompWithDis2Przed()
        {
            Invoice a = new Invoice("Faktura zapłaty3", ClientObjectMothers.CreateClientCompanyWithDiscountZnizka());
            a.AddProduct(ProductObjectMothers.CreateProductPrzedmiotEUR(), 1);
            a.AddProduct(ProductObjectMothers.CreateProductPrzedmiotPLN(), 2);
            return a;
        }
        public static Invoice CreateInvoiceCompWithDis1USD1PLN()
        {
            Invoice a = new Invoice("Faktura zapłaty2", ClientObjectMothers.CreateClientPrivateWithDiscountNetto());
            a.AddProduct(ProductObjectMothers.CreateProductUslugaPLN(), 2);
            a.AddProduct(ProductObjectMothers.CreateProductUslugaUSD(), 3);
            return a;
        }
        public static Item CreateItems123()
        {
            Item a = new Item(ProductObjectMothers.CreateProductPrzedmiotEUR(), 123);
            return a;
        }
    }
}
