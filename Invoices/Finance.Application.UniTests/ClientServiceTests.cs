using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Invoices.Domain.Model.Client.Repositories;
using Finance.Application;
using Invoices.Domain.Model.Client;
using Moq;
using Finance.ObjectMothers;

namespace Finance.Application.UniTests
{
    [TestClass]
    public class ClientServiceTests
    {
        [TestMethod]
        public void ClientDocCreateTest()
        {
            // Arrange
            Mock<IClientRepositories> repositoryMock = new Mock<IClientRepositories>();
            Mock<ICompanyRepositories> repositoryMock2 = new Mock<ICompanyRepositories>();
            IClientService bs = new ClientService(repositoryMock.Object, repositoryMock2.Object);
            repositoryMock.Setup(m => m.FindId(It.IsAny<int>())).Returns(ClientObjectMothers.CreateClientPrivateWithDiscountNetto());
            // Act
            bs.CreateDocPrivate(12);

            // Assert
            repositoryMock.Verify(k => k.FindId(It.IsAny<int>()), Times.Once());
        }
        [TestMethod]
        public void CompanyDocCreateTest()
        {
            // Arrange
            Mock<IClientRepositories> repositoryMock = new Mock<IClientRepositories>();
            Mock<ICompanyRepositories> repositoryMock2 = new Mock<ICompanyRepositories>();
            IClientService bs = new ClientService(repositoryMock.Object, repositoryMock2.Object);
            repositoryMock2.Setup(m => m.FindID(It.IsAny<int>())).Returns(ClientObjectMothers.CreateClientCompanyWithoutDiscount());
            // Act
            bs.CreateDocCompany(12);

            // Assert
            repositoryMock2.Verify(k => k.FindID(It.IsAny<int>()), Times.Once());
        }

    }
}
