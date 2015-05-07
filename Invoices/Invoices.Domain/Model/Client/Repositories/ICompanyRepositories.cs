using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iesi = Iesi.Collections.Generic;
using collections = System.Collections.Generic;

namespace Invoices.Domain.Model.Client.Repositories
{
    public interface ICompanyRepositories
    {
        void InsertCompany(Company client);
        void DeleteCompany(int id);
        Company FindID(int Id);
        Company FindNip(NIP nip);
        Company FindRegon(Regon regon);
        collections.List<Company> FindNameC(string name);
        collections.List<Company> FindAllC();
        iesi.ISet<Discount> GetAllDiscountC(int id);
        iesi.ISet<Contact> GetAllContactsC(int id);
    }
}
