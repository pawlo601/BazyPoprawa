using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Invoices.Domain.Model.Invoice;
using Invoices.Domain.Model.Invoice.Repositories;
using Finance.Infrastructure.DataBase;

namespace DataBaseTests
{
    [TestClass]
    public class InvoiceDataBaseTests
    {
        private InvoiceDataBaseIM baza = new InvoiceDataBaseIM();
        [TestMethod]
        public void FindTest()
        {
            Assert.IsNotNull(baza.Find("FAK.134.23.4.33.22249"));
        }
        [TestMethod]
        public void InsertTest()
        {
            Invoice a = new Invoice();
            baza.Insert(a);
            Invoice b = baza.Find(a.ID);
            Assert.IsNotNull(b);
        }
        [TestMethod]
        public void DeleteTest()
        {
            Invoice a = new Invoice();
            baza.Insert(a);
            baza.Delete(a.ID);
            Invoice b = baza.Find(a.ID);
            Assert.IsNull(b);
        }
    }
}
