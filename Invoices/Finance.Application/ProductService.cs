using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Domain.Model.Product;
using Invoices.Domain.Model.Product.Repositories;
using Finance.Infrastructure.Repositories;

namespace Finance.Application
{
    public class ProductService:IProductService
    {
        private IProductRepositories repo;
        public ProductService()
        {
            repo = new ProductIM();
        }
        public ProductService(IProductRepositories re)
        {
            repo = re;
        }
        public void CreateDoc(string nameOfProduct)
        {
            Product a = repo.FindProduct(nameOfProduct);
            string path = @"c:\bazy\";
            path += a.Name + ".txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(a.FormatString());
                }
            }
        }
        public Money Exchange(Money pieniadz, Waluta wal)
        {
            Money a = new Money(pieniadz.Value, pieniadz.NameOfCurrency);
            a.ChengeCurrency(wal);
            return a;
        }
        public List<Currency> GetCurrency()
        {
            Money a = new Money();
            return a.Curr.ListOfCurrency;
        }
        public double GetExchange(Waluta from, Waluta to)
        {
            return new Money().Curr.Swap(from, to);
        }
    }
}
