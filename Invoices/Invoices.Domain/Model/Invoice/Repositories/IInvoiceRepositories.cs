using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Domain.Model.Invoice.Repositories
{
    public interface IInvoiceRepositories
    {
        void Insert(Invoice invoice);
        void Delete(string Id);
        Invoice Find(string Id);
        List<Invoice> FindAll();
        List<Invoice> FindAllPerContractor(int idOfContractor);
        List<Invoice> FindAllPerContractor(string nameCompany);
        List<Invoice> FindAllPerContractor(string name, string surname);
        List<Invoice> FindAllPerData(DateTime date);
    }
}
