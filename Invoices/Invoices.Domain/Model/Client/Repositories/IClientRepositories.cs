using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iesi = Iesi.Collections.Generic;
using collections = System.Collections.Generic;

namespace Invoices.Domain.Model.Client.Repositories
{
    public interface IClientRepositories
    {
        void InsertClient(Client client);
        void DeleteClient(int id);
        Client FindId(int Id);
        collections.List<Client> FindName(string name, string surname);
        collections.List<Client> FindAll();
        iesi.ISet<Discount> GetAllDiscount(int IdClient);
    }
}
