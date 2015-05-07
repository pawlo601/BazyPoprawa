using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Invoices.Domain.Model.Client;
using Finance.ObjectMothers;

namespace Finance.Domain.UniTests
{

    [TestClass]
    public class ClientTest
    {
        [TestMethod]
        public void ChangeTypOfBonus()
        {
            Discount dis = ClientObjectMothers.CreateDiscountNetto();
            bool pomoc = false;
            if (dis.Type.Equals(Bonus.Netto))
                pomoc = true;
            Assert.IsTrue(pomoc);
            pomoc = false;
            dis.ChangeType(Bonus.Zniżka, 0.1f);
            if (dis.Type.Equals(Bonus.Zniżka))
                pomoc = true;
            Assert.IsTrue(pomoc);
        }
        [TestMethod]
        public void ChangeBonusThrowException()
        {
            Discount dis = ClientObjectMothers.CreateDiscountZnizka();
            try
            {
                dis.ChangeBonus(-1.0f);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Niewłaściwy bonus.\n"));
            }
        }
        [TestMethod]
        public void NipThrowExceptionToLongNip()
        {
            try
            {
                NIP np = new NIP("1123456789012");
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Błąd w NIP-ie. Zła długość.\n"));
            }
        }
        [TestMethod]
        public void NipThrowExceptionWrongNumber()
        {
            try
            {
                NIP np = new NIP("123456789A");
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Błąd w NIP-ie.Niewłaściwe znaki.\n"));
            }
        }
        [TestMethod]
        public void NipThrowExceptionWrongNIP()
        {
            try
            {
                NIP np = new NIP("1234567890");
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Błąd w NIP-ie. Niepoprawne znaczenie.\n"));
            }
        }
        [TestMethod]
        public void RegonThrowExceptionWrongRegon1()
        {
            try
            {
                Regon reg = new Regon("000");
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Błąd w Regonie-ie.\n"));
            }
        }
        [TestMethod]
        public void RegonThrowExceptionWrongRegon2()
        {
            try
            {
                Regon reg = new Regon("0002363457456757");
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Błąd w Regonie-ie.\n"));
            }
        }
        [TestMethod]
        public void RegonThrowExceptionWrongRegon3()
        {
            try
            {
                Regon reg = new Regon("123456789");
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Błąd w Regonie-ie. Błędne znaczenie\n"));
            }
        }
        [TestMethod]
        public void RegonThrowExceptionWrongRegon4()
        {
            try
            {
                Regon reg = new Regon("123456A89");
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Błąd w Regonie-ie. Niewłaściwe znaki.\n"));
            }
        }
        [TestMethod]
        public void RegonThrowExceptionWrongRegon5()
        {
            try
            {
                Regon reg = new Regon("12345678901234");
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Błąd w Regonie-ie. Błędne znaczenie\n"));
            }
        }
        [TestMethod]
        public void RegonThrowExceptionWrongRegon6()
        {
            try
            {
                Regon reg = new Regon("12Ae5678901234");
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Błąd w Regonie-ie. Niewłaściwe znaki.\n"));
            }
        }
        [TestMethod]
        public void Client()
        {
            Client a = ClientObjectMothers.CreateClientPrivateWithoutDiscount();
            Assert.IsTrue(a.ListOfDiscount.Count == 0);

            a.AddDiscount(ClientObjectMothers.CreateDiscountNetto());
            Assert.IsTrue(a.ListOfDiscount.Count == 1);

            a.AddContact(ClientObjectMothers.CreateMail());
            Assert.IsTrue(a.ListOfContact.Count == 1);

        }
        [TestMethod]
        public void Contact1()
        {
            try
            {
                Contact a = new Contact("+48", "1234568;");
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void Contact2()
        {
            try
            {
                Contact a = new Contact("+48", "123456890");
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void Contact3()
        {
            try
            {
                Contact a = new Contact("zlymail");
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
