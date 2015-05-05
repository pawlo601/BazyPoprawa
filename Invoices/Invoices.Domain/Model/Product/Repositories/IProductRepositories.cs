using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Domain.Model.Product.Repositories
{
    public interface IProductRepositories
    {
        void InsertProduct(Product product);
        void InsertCurrency(Currency cuurr);
        void DeleteProduct(int Id);
        void DeleteProduct(string name);
        void DeleteCurrency(Waluta name);
        Product FindProduct(int Id);
        Product FindProduct(string name);
        Currency FindCurrency(Waluta Name);
        List<Currency> FindAllCurrencies();
        List<Product> FindAllProducts();
    }
}
