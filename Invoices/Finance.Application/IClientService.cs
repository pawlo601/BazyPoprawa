using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Domain.Model.Client;
using collections=System.Collections.Generic;
using iesi=Iesi.Collections.Generic;

namespace Finance.Application
{
    public interface IClientService
    {
        void CreateDocPrivate(int id);
        void CreateDocCompany(int id);
        collections.List<Contact> GetAllContactsPrivate(int id);
        collections.List<Contact> GetAllContactsCompany(int id);
        collections.List<Client> GetAllClients();
        collections.List<Company> GetAllCompany();
        collections.List<Discount> GetAllDiscountClient(int id);
        collections.List<Discount> GetAllDiscountCompany(int id);

    }
}
