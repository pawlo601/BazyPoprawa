using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Invoices.Domain.Model.Product;
using Invoices.Domain.Model.Product.Repositories;
using Finance.Infrastructure.DataBase;

namespace DataBaseTests
{
    [TestClass]
    public class ProductDataBaseTests
    {
        private ProductDataBaseIM baza = new ProductDataBaseIM();
        [TestMethod]
        public void FindProductTest()
        {
            Assert.IsNotNull(baza.FindProduct(1001));
        }
        [TestMethod]
        public void FindProductTest2()
        {
            Assert.IsNotNull(baza.FindProduct("Nazwa Produktu452"));
        }
        [TestMethod]
        public void FindCurrencyTest()
        {
            Assert.IsNotNull(baza.FindCurrency(Waluta.EUR));
        }
        [TestMethod]
        public void InsertProductTest()
        {
            Product a = new Product();
            baza.InsertProduct(a);
            Product b = baza.FindProduct(a.ID);
            Assert.IsNotNull(b);
        }
        [TestMethod]
        public void InsertCurrencyTest()
        {
            Currency a = new Currency() { Name=Waluta.EUR, ExchangeInTheRelationToPLN=4.0f};
            baza.InsertCurrency(a);
            Currency b = baza.FindCurrency(a.Name);
            Assert.IsNotNull(b);
        }
        [TestMethod]
        public void DeleteTest()
        {
            Product a = new Product();
            baza.InsertProduct(a);
            baza.DeleteProduct(a.ID);
            Product b = baza.FindProduct(a.ID);
            Assert.IsNull(b);
        }
        [TestMethod]
        public void DeleteTest2()
        {
            Product a = new Product();
            baza.InsertProduct(a);
            baza.DeleteProduct(a.Name);
            Product b = baza.FindProduct(a.ID);
            Assert.IsNull(b);
        }
        [TestMethod]
        public void DeleteTest3()
        {
            Currency a = new Currency();
            baza.InsertCurrency(a);
            baza.DeleteCurrency(a.Name);
            Product b = baza.FindProduct(a.ID);
            Assert.IsNull(b);
        }
        [TestMethod]
        public void FindAllTest1()
        {
            Assert.IsNotNull(baza.FindAllCurrencies());
        }
        [TestMethod]
        public void FindAllTest2()
        {
            Assert.IsNotNull(baza.FindAllProducts());
        }
    }
}
