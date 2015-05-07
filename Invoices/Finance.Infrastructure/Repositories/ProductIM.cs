using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Domain.Model.Product;
using Invoices.Domain.Model.Product.Repositories;

namespace Finance.Infrastructure.Repositories
{
    public class ProductIM:IProductRepositories
    {
        private List<Product> products = new List<Product>();
        private List<Currency> curr = new List<Currency>();

        public ProductIM()
        {
            Price c1 = new Price(10, Waluta.PLN, 0.19f);
            Product a1 = new Product("Pierwsza rzecz", TypProduktu.Usługa, c1);
            products.Add(a1);
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Price c2 = new Price(20, Waluta.EUR, 0.10f);
            Product a2 = new Product("Druga rzecz", TypProduktu.Przedmiot, c2);
            products.Add(a2);
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Price c3 = new Price(100, Waluta.USD, 0.19f);
            Product a3 = new Product("Trzecia rzecz", TypProduktu.Usługa, c3);
            products.Add(a3);
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Price c4 = new Price(5, Waluta.EUR, 0.2f);
            Product a4 = new Product("Druga rzecz", TypProduktu.Przedmiot, c4);
            products.Add(a4);
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Money a = new Money();
            curr = a.Curr.ListOfCurrency;
        }
        public void InsertProduct(Product product)
        {
            products.Add(product);
        }
        public void InsertCurrency(Currency cuurr)
        {
            curr.Add(cuurr);
        }
        public void DeleteProduct(int Id)
        {
            foreach (var a in products)
            {
                if (a.ID == Id)
                    products.Remove(a);
            }
        }
        public void DeleteProduct(string name)
        {
            foreach (var a in products)
            {
                if (a.Name == name)
                    products.Remove(a);
            }
        }
        public void DeleteCurrency(Waluta name)
        {
            foreach (var a in curr)
            {
                if (a.Name == name)
                    curr.Remove(a);
            }
        }
        public Product FindProduct(int Id)
        {
            foreach (Product a in products)
            {
                if (a.ID == Id)
                    return a;
            }
            return null;
        }
        public Product FindProduct(string name)
        {
            foreach (Product a in products)
            {
                if (a.Name == name)
                    return a;
            }
            return null;
        }
        public Currency FindCurrency(Waluta Name)
        {
            foreach (Currency a in curr)
            {
                if (a.Name == Name)
                    return a;
            }
            return null;
        }
        public List<Currency> FindAllCurrencies()
        {
            return curr;
        }
        public List<Product> FindAllProducts()
        {
            return products;
        }
    }
}
