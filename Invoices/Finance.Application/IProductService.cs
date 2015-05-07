using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Domain.Model.Product;

namespace Finance.Application
{
    public interface IProductService
    {
        void CreateDoc(string nameOfProduct);
        Money Exchange(Money pieniadz, Waluta wal);
        List<Currency> GetCurrency();
        double GetExchange(Waluta from, Waluta to);
    }
}
