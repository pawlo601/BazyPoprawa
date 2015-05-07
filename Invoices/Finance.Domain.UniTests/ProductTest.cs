using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Invoices.Domain.Model.Product;
using Finance.ObjectMothers;

namespace Finance.Domain.UniTests
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void PriceThrowException()
        {
            try
            {
                var a = new Price(12.05f, Waluta.PLN, 1.06f);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Zła wartośc VAT."));
            }
        }
        [TestMethod]
        public void MoneyThrowException()
        {
            try
            {
                var a = new Money(-12.05f, Waluta.PLN);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Zła wartość.\n"));
            }
        }
        [TestMethod]
        public void CommentsTest()
        {
            string a = "";
            for (int i = 0; i < 260; i++)
                a += "q";
            Product b = ProductObjectMothers.CreateProductPrzedmiotEUR();
            b.SetComments(a);
            Assert.AreEqual(b.Comments.Length, 250);
            b.Comments = a;
            Assert.AreEqual(b.Comments.Length, 250);
        }
        [TestMethod]
        public void GetCurrencyTest()
        {
            Product a = ProductObjectMothers.CreateProductPrzedmiotEUR();
            Assert.AreEqual(a.GetCurrency(), Waluta.EUR);
        }
        [TestMethod]
        public void ChangeCurrency()
        {
            Product a = ProductObjectMothers.CreateProductPrzedmiotEUR();
            float b = a.Price.NetPrice.Value;
            a.ChangeCurrency(Waluta.PLN);
            float c = a.Price.NetPrice.Value;
            Money d = ProductObjectMothers.CreateMoneyPLN();
            Assert.AreEqual(d.Curr.GetCourse(Waluta.EUR)*b,c);
        }
        [TestMethod]
        public void GetGrossTest()
        {
            Price a = ProductObjectMothers.CreatePricePLN();
            Money b = new Money(a.NetPrice.Value, a.NetPrice.NameOfCurrency);
            b.Value *= (a.VAT+1.0f);
            Assert.AreEqual(b.Value, a.GetGross().Value);
            Assert.AreEqual(b.NameOfCurrency, a.GetGross().NameOfCurrency);
        }
        [TestMethod]
        public void ChangeCurrency2()
        {
            Price a = ProductObjectMothers.CreatePriceUSD();
            Money b = new Money(a.NetPrice.Value, a.NetPrice.NameOfCurrency);
            a.ChangeCurrency(Waluta.EUR);
            b.Value *= (float)b.Curr.GetCourse(Waluta.USD)/(float)b.Curr.GetCourse(Waluta.EUR);
            b.NameOfCurrency = Waluta.EUR;
            Assert.AreEqual(a.NetPrice.Value, b.Value);
            Assert.AreEqual(a.NetPrice.NameOfCurrency, b.NameOfCurrency);
        }

    }
}
