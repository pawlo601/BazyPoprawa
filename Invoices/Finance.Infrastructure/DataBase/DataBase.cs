using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using Invoices.Domain.Model.Product;
using Invoices.Domain.Model.Product.Repositories;
using Invoices.Domain.Model.Client;
using Invoices.Domain.Model.Client.Repositories;
using Invoices.Domain.Model.Invoice;
using Invoices.Domain.Model.Invoice.Repositories;

namespace Finance.Infrastructure.DataBase
{
    public class DataBase
    {
        static ISession OpenSession()
        {
            return new Configuration().Configure().BuildSessionFactory().OpenSession();
        }
        static ISessionFactory OpenSessionFactory()
        {
            return new Configuration().Configure().BuildSessionFactory();
        }
        public void AddCurrencies()
        {
            ISessionFactory sessionFactory = OpenSessionFactory();
            using (IStatelessSession statelessSession = sessionFactory.OpenStatelessSession())
            using (ITransaction transaction = statelessSession.BeginTransaction())
            {
                foreach (Waluta a in Enum.GetValues(typeof(Waluta)))
                    statelessSession.Insert(new Currency() { Name = a, ExchangeInTheRelationToPLN = a.GetExchange() });
                transaction.Commit();
            }
        }
        public void AddProducts(int howMany)
        {
            ISessionFactory sessionFactory = OpenSessionFactory();
            using (IStatelessSession statelessSession = sessionFactory.OpenStatelessSession())
            using (ITransaction transaction = statelessSession.BeginTransaction())
            {
                for (int i = 0; i < howMany; i++)
                    statelessSession.Insert(new Product());
                transaction.Commit();
            }
        }
        public void AddClients(int howMany)
        {
            Client b;
            for(int i=0;i<howMany;i++)
            {
                b = new Client();
                b.AddSomeContacts();
                b.AddSomeDiscounts();
                using (var s = OpenSession())
                {
                    s.Save(b);
                    s.Flush();
                }
                System.Threading.Thread.Sleep(50);
                if (i % 100 == 0)
                    Console.WriteLine(i);
            }
        }
        public void AddCompanies(int howMany)
        {
            Company b;
            for (int i = 0; i < howMany; i++)
            {
                b = new Company();
                b.AddSomeContacts();
                b.AddSomeDiscounts();
                using (var s = OpenSession())
                {
                    s.Save(b);
                    s.Flush();
                }
                System.Threading.Thread.Sleep(50);
                if (i % 100 == 0)
                    Console.WriteLine(i);
            }
        }
        public void AddInvoices(int howMany)
        {
            Invoice b;
            for (int i = 0; i < howMany; i++)
            {
                b = new Invoice();
                b.AddSomeItems();
                using (var s = OpenSession())
                {
                    s.Save(b);
                    s.Flush();
                }
                if(i%10==0)
                    Console.WriteLine(i);
            }
        }
        public static void Main()
        {
            DataBase a = new DataBase();
            DateTime start = DateTime.Now;
            //a.AddCurrencies();
            //a.AddProducts(1000);
            //a.AddCompanies(1000);
            a.AddInvoices(1000);
            Console.WriteLine((DateTime.Now - start).ToString());
            Console.ReadKey();
        }
    }
}
