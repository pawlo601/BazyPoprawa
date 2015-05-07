using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Domain.Model.Client;
using Invoices.Domain.Model.Client.Repositories;
using iesi = Iesi.Collections.Generic;
using collections = System.Collections.Generic;

namespace Finance.Infrastructure.Repositories
{
    public class ClientIM:IClientRepositories,ICompanyRepositories
    {
        private collections.List<Client> clients = new collections.List<Client>();
        private collections.List<Company> companies = new collections.List<Company>();
        public ClientIM()
        {
            Address ad1 = new Address("S1", "12", "Wrocław", "12-456", "Poland");
            clients.Add(new Client("Imie","Nazwisko", ad1));
            Address ad2 = new Address();
            NIP nip = new NIP();
            Regon regon = new Regon();
            companies.Add(new Company("NazwaFirmy", ad2, nip, regon));
        }
        public void InsertClient(Client client)
        {
            clients.Add(client);
        }
        public void DeleteClient(int id)
        {
            foreach (var a in clients)
            {
                if (id == a.ID)
                {
                    clients.Remove(a);
                }
            }
        }
        public Client FindId(int id)
        {
            foreach (var a in clients)
            {
                if (id == a.ID)
                {
                    return a;
                }
            }
            return null;
        }
        public collections.List<Client> FindName(string name, string surname)
        {
            collections.List<Client> list = new collections.List<Client>();
            foreach (var a in clients)
            {
                if (a.Name==name&&a.Surname==surname)
                {
                    list.Add(a);
                }
            }
            return list;
        }
        public collections.List<Client> FindAll()
        {
            return clients;
        }
        public iesi.ISet<Discount> GetAllDiscount(int id)
        {
            foreach (var a in clients)
            {
                if (a.ID==id)
                {
                    return a.ListOfDiscount;
                }
            }
            return null;
        }
        public iesi.ISet<Contact> GetAllContacts(int id)
        {
            foreach (var a in clients)
            {
                if (a.ID == id)
                {
                    return a.ListOfContact;
                }
            }
            return null;
        }
        public void InsertCompany(Company client)
        {
            companies.Add(client);
        }
        public void DeleteCompany(int id)
        {
            foreach (var a in companies)
            {
                if (id == a.ID)
                {
                    clients.Remove(a);
                }
            }
        }
        public Company FindID(int id)
        {
            foreach (var a in companies)
            {
                if (id == a.ID)
                {
                    return a;
                }
            }
            return null;
        }
        public Company FindNip(NIP nip)
        {
            foreach (var a in companies)
            {
                if (nip==a.Nip)
                {
                    return a;
                }
            }
            return null;
        }
        public Company FindRegon(Regon regon)
        {
            foreach (var a in companies)
            {
                if (regon == a.Regon)
                {
                    return a;
                }
            }
            return null;
        }
        public collections.List<Company> FindNameC(string name)
        {
            collections.List<Company> list = new collections.List<Company>();
            foreach (var a in companies)
            {
                if (a.Name==name)
                {
                    list.Add(a);
                }
            }
            return list;
        }
        collections.List<Company> ICompanyRepositories.FindAllC()
        {
            return companies;
        }
        public iesi.ISet<Discount> GetAllDiscountC(int id)
        {
            foreach (var a in companies)
            {
                if (a.ID==id)
                {
                    return a.ListOfDiscount;
                }
            }
            return null;
        }
        public iesi.ISet<Contact> GetAllContactsC(int id)
        {
            foreach (var a in companies)
            {
                if (a.ID == id)
                {
                    return a.ListOfContact;
                }
            }
            return null;
        }
    }
}
