using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Invoices.Domain.Model.Product.Repositories;
using Finance.Application;
using Invoices.Domain.Model.Product;
using Moq;
using Finance.ObjectMothers;


namespace Finance.Application.UniTests
{
    [TestClass]
    public class ProductServiceTests
    {
        [TestMethod]
        public void CeateDocProductTest()
        {
            Mock<IProductRepositories> repositoryMock = new Mock<IProductRepositories>();
            IProductService bs = new ProductService(repositoryMock.Object);
            repositoryMock.Setup(m => m.FindProduct(It.IsAny<string>())).Returns(ProductObjectMothers.CreateProductPrzedmiotPLN());

            // Act
            bs.CreateDoc("nazwa");

            // Assert
            repositoryMock.Verify(k => k.FindProduct(It.IsAny<string>()), Times.Once());
        }
        [TestMethod]
        public void ExchangeTest()
        {
            ProductService a = new ProductService();
            Money b = new Money(12.0f, Waluta.EUR);
            Money c = a.Exchange(b, Waluta.PLN);
            Assert.AreEqual(c.Value, 12 * 4.0547962f);
            Assert.AreEqual(c.NameOfCurrency, Waluta.PLN);
        }
        [TestMethod]
        public void GetCurrencyTest()
        {
            ProductService a = new ProductService();
            List<Currency> list = a.GetCurrency();
            Assert.AreEqual(list.Count, Enum.GetValues(typeof(Waluta)).Length);
        }
        [TestMethod]
        public void GetExchangeTest()
        {
            ProductService a = new ProductService();
            double e = a.GetExchange(Waluta.EUR, Waluta.USD);
            Currencies z = new Money().Curr;
            Assert.AreEqual(e, z.GetCourse(Waluta.EUR) / z.GetCourse(Waluta.USD));
        }
    }
}
