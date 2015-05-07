using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Invoices.Domain.Model.Invoice.Repositories;
using Finance.Application;
using Invoices.Domain.Model.Invoice;
using Moq;
using Finance.ObjectMothers;

namespace Finance.Application.UniTests
{
    [TestClass]
    public class InvoiceServiceTests
    {
        [TestMethod]
        public void InvoiceDocCreateTest()
        {
            Mock<IInvoiceRepositories> repositoryMock = new Mock<IInvoiceRepositories>();
            IInvoiceServices bs = new InvoiceService(repositoryMock.Object);
            repositoryMock.Setup(m => m.Find(It.IsAny<string>())).Returns(InvoiceObjectMothers.CreateInvoiceCompWithDis1USD1PLN());

            // Act
            bs.CreateInvoice("jakies id");

            // Assert
            repositoryMock.Verify(k => k.Find(It.IsAny<string>()), Times.Once());
        }
    }
}
