using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
using System.Reflection;
using Invoices.Domain.Model.Product;
using Invoices.Domain.Model.Product.Repositories;

namespace Finance.Infrastructure.DataBase
{
    public class ProductDataBaseIM : IProductRepositories
    {
        static ISession OpenSession()
        {
            return new Configuration().Configure().BuildSessionFactory().OpenSession();
        }
        public void InsertProduct(Product product)
        {
            using (ISession s = OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    s.Save(product);
                    t.Commit();
                }
            }
        }
        public void InsertCurrency(Currency cuurr)
        {
            using (ISession s = OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    s.Save(cuurr);
                    t.Commit();
                }
            }
        }
        public void DeleteProduct(int Id)
        {
            using (var session = OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var queryString = string.Format("delete {0} where id = :id", typeof(Product));
                session.CreateQuery(queryString)
                       .SetParameter("id", Id)
                       .ExecuteUpdate();
                transaction.Commit();
            }
        }
        public void DeleteProduct(string name)
        {
            using (var session = OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var queryString = string.Format("delete {0} where name = :name", typeof(Product));
                session.CreateQuery(queryString)
                       .SetParameter("name", name)
                       .ExecuteUpdate();
                transaction.Commit();
            }
        }
        public void DeleteCurrency(Waluta name)
        {
            using (var session = OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var queryString = string.Format("delete {0} where Name = :name", typeof(Currency));
                session.CreateQuery(queryString)
                       .SetParameter("name", name)
                       .ExecuteUpdate();
                transaction.Commit();
            }
        }
        public Product FindProduct(int Id)
        {
            using (ISession s = OpenSession())
            {
                IQuery q = s.CreateQuery("FROM Product P WHERE P.id = " + Id.ToString());
                IList<Product> result = q.List<Product>();
                if (result.Count == 0)
                    return null;
                else
                {
                    foreach (Product a in result)
                        return a;
                }
            }
            return null;
        }
        public Product FindProduct(string name)
        {
            using (ISession s = OpenSession())
            {
                IQuery q = s.CreateQuery("FROM Product P WHERE P.Name=:n");
                q.SetParameter("n", name);
                IList<Product> result = q.List<Product>();
                if (result.Count == 0)
                    return null;
                else
                {
                    foreach (Product a in result)
                        return a;
                }
            }
            return null;
        }
        public Currency FindCurrency(Waluta Name)
        {
            using (ISession s = OpenSession())
            {
                IQuery q = s.CreateQuery("FROM Currency P WHERE P.Name=:n" );
                q.SetParameter("n", Name);
                IList<Currency> result = q.List<Currency>();
                if (result.Count == 0)
                    return null;
                else
                {
                    foreach (Currency a in result)
                        return a;
                }
            }
            return null;
        }
        public List<Currency> FindAllCurrencies()
        {
            List<Currency> result2 = new List<Currency>();
            using (ISession s = OpenSession())
            {
                IQuery q = s.CreateQuery("from Currency as j order by j.Name");
                IList<Currency> result = q.List<Currency>();
                foreach (Currency a in result)
                {
                    result2.Add(a);
                }
            }
            return result2;
        }
        public List<Product> FindAllProducts()
        {
            List<Product> result2 = new List<Product>();
            using (ISession s = OpenSession())
            {
                IQuery q = s.CreateQuery("from Product as j order by j.Name");
                IList<Product> result = q.List<Product>();
                foreach (Product a in result)
                {
                    result2.Add(a);
                }
            }
            return result2;
        }
    }
}
