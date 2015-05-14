using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Engine;
using System.Reflection;
using Invoices.Domain.Model.Client;
using Invoices.Domain.Model.Client.Repositories;
using iesi = Iesi.Collections.Generic;
using collections = System.Collections.Generic;

namespace Finance.Infrastructure.DataBase
{
    public class ClientDataBaseIM : IClientRepositories, ICompanyRepositories
    {
        static ISession OpenSession()
        {
            return new Configuration().Configure().BuildSessionFactory().OpenSession();
        }
        public void InsertClient(Client client)
        {
            using (ISession s = OpenSession())
            {
                s.Save(client);
                s.Flush();
            }
        }
        public void InsertCompany(Company client)
        {
            InsertClient(client);
        }
        public void DeleteClient(int id)
        {
            using (var session = OpenSession())
            {
                Client a = FindId(id);

                var sql = String.Format("delete from LOCALISATION WHERE id = {0}",a.Localisation.ID.ToString());
                var query = session.CreateSQLQuery(sql);
                var result = query.UniqueResult();

                sql = String.Format("delete from CLIENT  WHERE id = {0}", id.ToString());
                query = session.CreateSQLQuery(sql);
                result = query.UniqueResult();

                sql = String.Format("delete from CONTACTS  WHERE ID_Client = {0}", id.ToString());
                query = session.CreateSQLQuery(sql);
                result = query.UniqueResult();

                sql = String.Format("delete from DISCOUNTS  WHERE ID_Client = {0}", id.ToString());
                query = session.CreateSQLQuery(sql);
                result = query.UniqueResult();
            }
        }
        public void DeleteCompany(int id)
        {
            using (var session = OpenSession())
            {
                Company a = FindID(id);

                var sql = String.Format("delete from LOCALISATION WHERE id = {0}", a.Localisation.ID.ToString());
                var query = session.CreateSQLQuery(sql);
                var result = query.UniqueResult();

                sql = String.Format("delete from COMPANY  WHERE id = {0}", id.ToString());
                query = session.CreateSQLQuery(sql);
                result = query.UniqueResult();

                sql = String.Format("delete from CONTACTS  WHERE ID_Client = {0}", id.ToString());
                query = session.CreateSQLQuery(sql);
                result = query.UniqueResult();

                sql = String.Format("delete from DISCOUNTS  WHERE ID_Client = {0}", id.ToString());
                query = session.CreateSQLQuery(sql);
                result = query.UniqueResult();
            }
        }
        public iesi.ISet<Discount> GetAllDiscount(int id)
        {
            iesi.ISet<Discount> result2 = new iesi.HashedSet<Discount>();
            using (ISession s = OpenSession())
            {
                collections.IList<object> con = s.CreateSQLQuery("select idProduct from DISCOUNTS where ID_Client = " + id.ToString()).List<object>();
                int[] t1 = new int[con.Count];
                int i=0;
                foreach(object a in con)
                {
                    t1[i] = Convert.ToInt32(a);
                    i++;
                }

                con = s.CreateSQLQuery("select valueOfBonus from DISCOUNTS where ID_Client = " + id.ToString()).List<object>();
                float[] t2 = new float[con.Count];
                i = 0;
                foreach (object a in con)
                {
                    t2[i] = (float)Convert.ToDouble(a);
                    i++;
                }

                con = s.CreateSQLQuery("select type from DISCOUNTS where ID_Client = " + id.ToString()).List<object>();
                Bonus[] t3 = new Bonus[con.Count];
                i = 0;
                String c;
                foreach (object a in con)
                {
                    c = a as String;
                    if (c == "Netto")
                        t3[i] = Bonus.Netto;
                    else
                        t3[i] = Bonus.Zniżka;
                    i++;
                }
                for (i = 0; i < con.Count; i++)
                    result2.Add(new Discount(t1[i], t3[i], t2[i]));
            }
            return result2;
        }
        public iesi.ISet<Contact> GetAllContacts(int id)
        {
            iesi.ISet<Contact> result2 = new iesi.HashedSet<Contact>();
            using (ISession s = OpenSession())
            {
                collections.IList<object> con = s.CreateSQLQuery("select contact from CONTACTS where ID_Client = "+id.ToString()).List<object>();
                foreach (object a in con)
                    result2.Add(new Contact() { ContactTo = a as String });
            }
            return result2;
        }
        public iesi.ISet<Discount> GetAllDiscountC(int id)
        {
            return GetAllDiscount(id);
        }
        public iesi.ISet<Contact> GetAllContactsC(int id)
        {
            return GetAllContacts(id);
        }
        public Client FindId(int Id)
        {
            return FindIdLazy(Id);
            //return FindIdEager(Id);
        }
        private Client FindIdLazy(int Id)
        {
            using (ISession s = OpenSession())
            {
                IQuery q = s.CreateQuery("FROM Client P WHERE P.ID = " + Id.ToString());
                System.Collections.Generic.IList<Client> result = q.List<Client>();
                if (result.Count == 0)
                    return null;
                else
                {
                    foreach (Client a in result)
                    {
                        a.Localisation.FormatString();
                        return a;
                    }

                }
            }
            return null;
        }
        private Client FindIdEager(int Id)
        {
            Client a = FindIdLazy(Id);
            if(a!=null)
            {
                a.ListOfContact = GetAllContacts(Id);
                a.ListOfDiscount = GetAllDiscount(Id);
            }
            return a;
        }
        public Company FindID(int Id)
        {
            return FindIDLazy(Id);
            //return FindIDEager(Id);
        }
        private Company FindIDLazy(int Id)
        {
            using (ISession s = OpenSession())
            {
                IQuery q = s.CreateQuery("FROM Company P WHERE P.id = " + Id.ToString());
                collections.IList<Company> result = q.List<Company>();
                if (result.Count == 0)
                    return null;
                else
                {
                    foreach (Company a in result)
                    {
                        a.Localisation.FormatString();
                        return a;
                    }
                }
            }
            return null;
        }
        private Company FindIDEager(int Id)
        {
            Company a = FindIDLazy(Id);
            if (a != null)
            {
                a.ListOfContact = GetAllContactsC(Id);
                a.ListOfDiscount = GetAllDiscountC(Id);
            }
            return a;
        }
        public collections.List<Client> FindName(string name, string surname)
        {
            return FindNameLazy(name, surname);
            return FindNameEager(name, surname);
        }
        private collections.List<Client> FindNameLazy(string name, string surname)
        {
            using (ISession s = OpenSession())
            {
                IQuery q = s.CreateQuery("FROM Client P WHERE P.name = " + name + " and P.surname = " + surname);
                collections.IList<Client> result = q.List<Client>();
                if (result.Count == 0)
                    return null;
                else
                {
                    collections.List<Client> b = new collections.List<Client>();
                    foreach (Client a in result)
                    {
                        a.Localisation.FormatString();
                        b.Add(a);
                    }
                    return b;
                }
            }
        }
        private collections.List<Client> FindNameEager(string name, string surname)
        {
            collections.List<Client> a = FindNameLazy(name, surname);
            if (a != null)
            {
                foreach (Client b in a)
                {
                    b.ListOfContact = GetAllContacts(b.ID);
                    b.ListOfDiscount = GetAllDiscount(b.ID);
                }
            }
            return a;
        }
        public collections.List<Company> FindNameC(string name)
        {
            return FindNameCLazy(name);
            return FindNameCEager(name);
        }
        private collections.List<Company> FindNameCLazy(string name)
        {
            using (ISession s = OpenSession())
            {
                IQuery q = s.CreateQuery("FROM Company P WHERE P.name = " + name);
                collections.IList<Company> result = q.List<Company>();
                if (result.Count == 0)
                    return null;
                else
                {
                    collections.List<Company> b = new collections.List<Company>();
                    foreach (Company a in result)
                    {
                        a.Localisation.FormatString();
                        b.Add(a);
                    }
                    return b;
                }
            }
        }
        private collections.List<Company> FindNameCEager(string name)
        {
            collections.List<Company> a = FindNameCLazy(name);
            if (a != null)
            {
                foreach (Company b in a)
                {
                    b.ListOfContact = GetAllContactsC(b.ID);
                    b.ListOfDiscount = GetAllDiscountC(b.ID);
                }
            }
            return a;
        }
        public Company FindNip(NIP nip)
        {
            return FindNipLazy(nip);
            return FindNipEager(nip);
        }
        private Company FindNipLazy(NIP nip)
        {
            using (ISession s = OpenSession())
            {
                IQuery q = s.CreateQuery("FROM Company P WHERE P.NumerNip = " + nip);
                collections.IList<Company> result = q.List<Company>();
                if (result.Count == 0)
                    return null;
                else
                {
                    foreach (Company a in result)
                    {
                        a.Localisation.FormatString();
                        return a;
                    }
                }
                return null;
            }
        }
        private Company FindNipEager(NIP nip)
        {
            Company a = FindNipLazy(nip);
            if (a != null)
            {
                a.ListOfContact = GetAllContactsC(a.ID);
                a.ListOfDiscount = GetAllDiscountC(a.ID);
            }
            return a;
        }
        public Company FindRegon(Regon regon)
        {
            return FindRegonLazy(regon);
            return FindRegonEager(regon);
        }
        private Company FindRegonLazy(Regon regon)
        {
            using (ISession s = OpenSession())
            {
                IQuery q = s.CreateQuery("FROM Company P WHERE P.NumerRegon = " + regon);
                collections.IList<Company> result = q.List<Company>();
                if (result.Count == 0)
                    return null;
                else
                {
                    foreach (Company a in result)
                    {
                        a.Localisation.FormatString();
                        return a;
                    }
                }
                return null;
            }
        }
        private Company FindRegonEager(Regon regon)
        {
            Company a = FindRegonLazy(regon);
            if (a != null)
            {
                a.ListOfContact = GetAllContactsC(a.ID);
                a.ListOfDiscount = GetAllDiscountC(a.ID);
            }
            return a;
        }
        public collections.List<Client> FindAll()
        {
            collections.List<Client> result2 = new collections.List<Client>();
            using (ISession s = OpenSession())
            {
                IQuery q = s.CreateQuery("from Client as j order by j.name");
                collections.IList<Client> result = q.List<Client>();
                foreach (Client a in result)
                {
                    a.ListOfContact = GetAllContacts(a.ID);
                    a.ListOfDiscount = GetAllDiscount(a.ID);
                    result2.Add(a);
                }
            }
            return result2;
        }
        public collections.List<Company> FindAllC()
        {
            collections.List<Company> result2 = new collections.List<Company>();
            using (ISession s = OpenSession())
            {
                IQuery q = s.CreateQuery("from Company as j order by j.name");
                collections.IList<Company> result = q.List<Company>();
                foreach (Company a in result)
                {
                    a.ListOfContact = GetAllContactsC(a.ID);
                    a.ListOfDiscount = GetAllDiscountC(a.ID);
                    result2.Add(a);
                }
            }
            return result2;
        }
    }
}
