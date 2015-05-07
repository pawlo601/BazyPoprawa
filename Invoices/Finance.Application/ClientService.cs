using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Domain.Model.Client;
using Invoices.Domain.Model.Client.Repositories;
using Finance.Infrastructure.Repositories;
using iesi = Iesi.Collections.Generic;
using collections = System.Collections.Generic;


namespace Finance.Application
{
    public class ClientService : IClientService
    {
        private IClientRepositories repoClient;
        private ICompanyRepositories repoCompany;

        public ClientService()
        {
            var a = new ClientIM();
            repoClient = a;
            repoCompany = a;
        }
        public ClientService(IClientRepositories re, ICompanyRepositories re2)
        {
            repoClient = re;
            repoCompany = re2;
        }
        public void CreateDocPrivate(int id)
        {
            Client a = repoClient.FindId(id);
            string path = @"c:\bazy\";
            path += a.ID.ToString() + ".txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(a.FormatString());
                }
            }
        }
        public void CreateDocCompany(int id)
        {
            Company a = repoCompany.FindID(id);
            string path = @"c:\bazy\";
            path += a.ID.ToString() + ".txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(a.FormatString());
                }
            }
        }
        public collections.List<Contact> GetAllContactsPrivate(int id)
        {
            iesi.ISet<Contact> list = repoClient.GetAllContacts(id);
            collections.List<Contact> lista2 = new collections.List<Contact>();
            foreach (Contact a in list)
                lista2.Add(a);
            return lista2;
        }
        public collections.List<Contact> GetAllContactsCompany(int id)
        {
            iesi.ISet<Contact> list = repoCompany.GetAllContactsC(id);
            collections.List<Contact> lista2 = new collections.List<Contact>();
            foreach (Contact a in list)
                lista2.Add(a);
            return lista2;
        }
        public collections.List<Client> GetAllClients()
        {
            return repoClient.FindAll();
        }
        public collections.List<Company> GetAllCompany()
        {
            return repoCompany.FindAllC();
        }
        public collections.List<Discount> GetAllDiscountClient(int id)
        {
            iesi.ISet<Discount> list = repoClient.GetAllDiscount(id);
            collections.List<Discount> lista2 = new collections.List<Discount>();
            foreach (Discount a in list)
                lista2.Add(a);
            return lista2;
        }
        public collections.List<Discount> GetAllDiscountCompany(int id)
        {
            iesi.ISet<Discount> list = repoCompany.GetAllDiscountC(id);
            collections.List<Discount> lista2 = new collections.List<Discount>();
            foreach (Discount a in list)
                lista2.Add(a);
            return lista2;
        }
    }
}
