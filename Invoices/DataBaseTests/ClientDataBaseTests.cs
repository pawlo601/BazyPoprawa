using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Invoices.Domain.Model.Client;
using Invoices.Domain.Model.Client.Repositories;
using Finance.Infrastructure.DataBase;

namespace DataBaseTests
{
    [TestClass]
    public class ClientDataBaseTests
    {
        private ClientDataBaseIM baza = new ClientDataBaseIM();
        [TestMethod]
        public void FindIdTest()
        {
            Assert.IsNotNull(baza.FindId(2027));
        }
        [TestMethod]
        public void FindIDTest()
        {
            Assert.IsNotNull(baza.FindID(2030));
        }
        [TestMethod]
        public void InsertClientTest()
        {
            Client a = new Client();
            a.AddSomeDiscounts();
            baza.InsertClient(a);
            Client b = baza.FindId(a.ID);
            Assert.IsNotNull(b);
        }
        [TestMethod]
        public void InsertCompanyTest()
        {
            Company a = new Company();
            a.AddSomeDiscounts();
            baza.InsertCompany(a);
            Client b = baza.FindID(a.ID);
            Assert.IsNotNull(b);
        }
        [TestMethod]
        public void DeleteClientTest()
        {
            Client a = new Client();
            baza.InsertClient(a);
            baza.DeleteClient(a.ID);
            Assert.IsNull(baza.FindId(a.ID));
        }
        [TestMethod]
        public void DeleteCompanyTest()
        {
            Company a = new Company();
            baza.InsertCompany(a);
            baza.DeleteCompany(a.ID);
            Assert.IsNull(baza.FindID(a.ID));
        }
    }
}
