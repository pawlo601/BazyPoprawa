using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Invoices.Domain.Model.Invoice;
using Finance.ObjectMothers;
using produt = Invoices.Domain.Model.Product;

namespace Finance.Domain.UniTests
{
    [TestClass]
    public class InvoiceTest
    {
        [TestMethod]
        public void Item()
        {
            Item a = InvoiceObjectMothers.CreateItems123();
            Assert.IsTrue(a.Volume == 123);
            try
            {
                a.ChangeVolume(-12);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Zła ilość produktów.\n"));
            }
            a.Count();
            Assert.IsNotNull(a.Cost);
        }
        [TestMethod]
        public void Invoice()
        {
            Invoice a = InvoiceObjectMothers.CreateInvoicePrivWithDis2PrzedEUR();

            DateTime b = DateTime.Now;
            a.DateOfCreate = b;
            Assert.IsTrue(a.DateOfCreate.Equals(b));

            string tit = "tyty";
            a.ChengeTitle(tit);
            Assert.IsTrue(a.Title.Equals(tit));

            tit = "";
            for (int i = 0; i < 40; i++)
                tit += "q";
            a.Title = tit;
            Assert.AreEqual(a.Title.Length, 30);

            tit = "";
            for (int i = 0; i < 300; i++)
                tit += "q";
            a.Comments = tit;
            Assert.AreEqual(a.Comments.Length, 250);

            Assert.AreEqual(a.ListOfProducts.Count, 2);
            a.AddSomeItems();
            Assert.IsTrue(2<a.ListOfProducts.Count);
        
        }
        [TestMethod]
        public void PodsumowanieTest()
        {
            Invoice a = InvoiceObjectMothers.CreateInvoiceCompWithDis1USD1PLN();
            List<produt.Money> lista = a.Podsumowanie();
            foreach (produt.Money b in lista)
                Console.WriteLine(b.ToString());
            Assert.AreEqual(lista.Count, 2);
        }
    }
}
