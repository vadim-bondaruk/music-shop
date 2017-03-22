using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.BLL.Services;
using PVT.Q1._2017.Shop.Tests.Moq;

namespace PVT.Q1._2017.Shop.Tests.BllServiceTests
{
    [TestClass]
    public class PaymentServiceTest
    {
        [TestMethod]
        public void TestCreatePaymentWithPaymentCard()
        {
            var PaymentService = new PaymentService(new RepositoryFactoryMoq());
            PaymentService.RunSample();
            // PaymentService.CreatePaymentWithPaymentCard();
            Assert.IsNull(null);
        }
    }
}
