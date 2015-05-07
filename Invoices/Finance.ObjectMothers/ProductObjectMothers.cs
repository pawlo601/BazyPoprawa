using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Domain.Model.Product;

namespace Finance.ObjectMothers
{
    public class ProductObjectMothers
    {
        public static Product CreateProductPrzedmiotPLN()
        {
            Money mon = new Money(5000.0f, Waluta.PLN);
            Price pr = new Price(mon, 0.19f);
            Product a = new Product("Asus", TypProduktu.Przedmiot, pr);
            return a;
        }
        public static Product CreateProductPrzedmiotEUR()
        {
            Money mon = new Money(2000.0f, Waluta.EUR);
            Price pr = new Price(mon, 0.19f);
            Product a = new Product("Acer", TypProduktu.Przedmiot, pr);
            return a;
        }
        public static Product CreateProductUslugaPLN()
        {
            Money mon = new Money(30.0f, Waluta.PLN);
            Price pr = new Price(mon, 0.19f);
            Product a = new Product("Dowóz", TypProduktu.Usługa, pr);
            return a;
        }
        public static Product CreateProductUslugaUSD()
        {
            Money mon = new Money(900.0f, Waluta.USD);
            Price pr = new Price(mon, 0.19f);
            Product a = new Product("Ubezpieczenie", TypProduktu.Usługa, pr);
            return a;
        }
        public static Price CreatePricePLN()
        {
            Price pr = new Price(12.04f, Waluta.PLN, 0.20f);
            return pr;
        }
        public static Price CreatePriceUSD()
        {
            Price pr = new Price(3.99f, Waluta.USD, 0.20f);
            return pr;
        }
        public static Currencies CreateCurrency()
        {
            Currencies cu = Currencies.GetInstance();
            return cu;
        }
        public static Money CreateMoneyPLN()
        {
            Money mon = new Money(123.70f, Waluta.PLN);
            return mon;
        }
        public static Money CreateMoneyUSD()
        {
            Money mon = new Money(34.61f, Waluta.USD);
            return mon;
        }
        public static Money CreateMoneyEUR()
        {
            Money mon = new Money(23.12f, Waluta.EUR);
            return mon;
        }
        public static Currency CreatePLN()
        {
            return new Currency();
        }
        public static Currency CreateEUR()
        {
            Currency a = new Currency();
            a.Name = Waluta.EUR;
            a.ExchangeInTheRelationToPLN = 3.89f;
            return a;
        }
        public static Currency CreateUSD()
        {
            Currency a = new Currency();
            a.Name = Waluta.USD;
            a.ExchangeInTheRelationToPLN = 3.19f;
            return a;
        }
    }
}
