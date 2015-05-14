using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
using System.Reflection;
using Invoices.Domain.Model.Invoice;
using Invoices.Domain.Model.Invoice.Repositories;
using iesi = Iesi.Collections.Generic;
using collections = System.Collections.Generic;

namespace Finance.Infrastructure.DataBase
{
    public class Change<T>
    {
        public static iesi.ISet<T> FromCollectionToIesi<T>(collections.List<T> a)
        {
            iesi.ISet<T> b = new iesi.HashedSet<T>();
            foreach (T elem in a)
                b.Add(elem);
            return b;
        }
    }
    public class InvoiceDataBaseIM : IInvoiceRepositories
    {
        static ISession OpenSession()
        {
            return new Configuration().Configure().BuildSessionFactory().OpenSession();
        }
        public void Insert(Invoice invoice)
        {
            using (ISession s = OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    s.Save(invoice);
                    t.Commit();
                }
            }
        }
        public void Delete(string Id)
        {
            using (var session = OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var queryString = string.Format("delete {0} where ID = :id", typeof(Invoice));
                session.CreateQuery(queryString)
                       .SetParameter("id", Id)
                       .ExecuteUpdate();
                transaction.Commit();

                var sql = String.Format("delete from ITEMS  WHERE ID_Invoice = '{0}'", @Id);
                var query = session.CreateSQLQuery(sql);
                var result = query.UniqueResult();
            }
        }
        public collections.List<Item> GetItems(string id)
        {
            collections.List<Item> lista = new collections.List<Item>();
            using (ISession s = OpenSession())
            {
                collections.IList<object> con = s.CreateSQLQuery("select idOfProduct from ITEMS where ID_Invoice = '" + id.ToString()+"'").List<object>();
                int[] t1 = new int[con.Count];
                int i = 0;
                foreach (object a in con)
                {
                    t1[i] = Convert.ToInt32(a);
                    i++;
                }

                con = s.CreateSQLQuery("select volume from ITEMS where ID_Invoice = '" + id.ToString()+"'").List<object>();
                int[] t2 = new int[con.Count];
                i = 0;
                foreach (object a in con)
                {
                    t2[i] = Convert.ToInt32(a);
                    i++;
                }
                ProductDataBaseIM x = new ProductDataBaseIM();
                for (i = 0; i < con.Count; i++)
                    lista.Add(new Item(x.FindProduct(t1[i]), t2[i]));
            }
            return lista;
        }
        public Invoice FindLazy(string Id)
        {
            using (ISession s = OpenSession())
            {
                IQuery q = s.CreateQuery("FROM Invoice P WHERE P.id = '" + Id.ToString()+"'");
                System.Collections.Generic.IList<Invoice> result = q.List<Invoice>();
                if (result.Count == 0)
                    return null;
                else
                {
                    foreach (Invoice a in result)
                    {
                        return a;
                    }
                }
            }
            return null;
        }
        public Invoice FindEager(string Id)
        {
            Invoice a = FindLazy(Id);
            ClientDataBaseIM b = new ClientDataBaseIM();
            Invoices.Domain.Model.Client.Client c = b.FindId(a.IdClient);
            if (c == null)
                c = b.FindID(a.IdClient);
            a.Contractor = c;
            a.ListOfProducts = Change<Item>.FromCollectionToIesi<Item>(GetItems(Id));
            return a;
        }
        public Invoice Find(string Id)
        {
            return FindLazy(Id);
            return FindEager(Id);
        }
        public collections.List<Invoice> FindAllPerContractorLazy(int idOfContractor)
        {
            using (ISession s = OpenSession())
            {
                IQuery q = s.CreateQuery("FROM Invoice P WHERE P.idClient = " + idOfContractor.ToString());
                collections.IList<Invoice> result = q.List<Invoice>();
                if (result.Count == 0)
                    return null;
                else
                {
                    collections.List<Invoice> a = new collections.List<Invoice>();
                    foreach (Invoice b in result)
                        a.Add(b);
                    return a;
                }
            }
        }
        public collections.List<Invoice> FindAllPerContractorEager(int idOfContractor)
        {
            collections.List<Invoice> a = FindAllPerContractorLazy(idOfContractor);
            foreach (Invoice b in a)
            {
                b.ListOfProducts = Change<Item>.FromCollectionToIesi<Item>(GetItems(b.ID));
            }
            return a;
        }
        public collections.List<Invoice> FindAllPerContractor(int idOfContractor)
        {
            return FindAllPerContractorLazy(idOfContractor);
        }
        public collections.List<Invoice> FindAllPerDataLazy(DateTime date)
        {
            using (ISession s = OpenSession())
            {
                int a = date.DayOfYear;
                int b = date.Hour;
                int c = date.Minute;
                int d = date.Second;
                string e = "FAK." + a.ToString() + "." +
                           b.ToString() + "." +
                           c.ToString() + "." +
                           d.ToString() + ".";
                IQuery q = s.CreateQuery("FROM Invoice P WHERE CHARINDEX('"+@e+"',id)=1");
                collections.IList<Invoice> result = q.List<Invoice>();
                if (result.Count == 0)
                    return null;
                else
                {
                    collections.List<Invoice> g = new collections.List<Invoice>();
                    foreach (Invoice h in result)
                        g.Add(h);
                    return g;
                }
            }
        }
        public collections.List<Invoice> FindAllPerDataEager(DateTime date)
        {
            collections.List<Invoice> a = FindAllPerDataLazy(date);
            foreach (Invoice b in a)
            {
                b.ListOfProducts = Change<Item>.FromCollectionToIesi<Item>(GetItems(b.ID));
            }
            return a;
        }
        public collections.List<Invoice> FindAllPerData(DateTime date)
        {
            return FindAllPerDataLazy(date);
        }
        public collections.List<Invoice> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
