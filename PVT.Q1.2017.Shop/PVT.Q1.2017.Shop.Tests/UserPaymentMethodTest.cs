namespace PVT.Q1._2017.Shop.Tests
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Repositories;

    [TestClass]
    public class UserPaymentMethodRepositoryTest
    {
        private static readonly DbContext _dbContext;

        static UserPaymentMethodRepositoryTest()
        {
           _dbContext = new Mock<DbContext>().Object;
        }

        [TestMethod]
        public void UserPaymentMethodRepository_GetById_Verify()
        {
            
            var id = 1;
            var expected = new UserPaymentMethod() { Alias = "SSS"};
            Mock<UserPaymentMethodRepository> mock = new Mock<UserPaymentMethodRepository>(_dbContext);
            mock.Setup(r => r.GetById(id)).Returns(expected).Verifiable();

            UserPaymentMethod payment = mock.Object.GetById(id);
            Assert.AreEqual(expected, payment);
            mock.Verify();
        }


        [TestMethod]
        public void UserPaymentMethodRepository_GetAll()
        {
            var data = new List<UserPaymentMethod>()
            {
                new UserPaymentMethod {Alias = "AAA"},
                new UserPaymentMethod {Alias = "BBB"},
                new UserPaymentMethod {Alias = "CCC"}
            };

            Mock<UserPaymentMethodRepository> mock = new Mock<UserPaymentMethodRepository>(_dbContext);
            mock.Setup(r => r.GetAll()).Returns(data).Verifiable();

            ICollection<UserPaymentMethod> paymentMethod = mock.Object.GetAll();
            Assert.AreEqual(data, paymentMethod);
            mock.Verify();
        }


        [TestMethod]
        public void UserPaymentMethodRepository_GetAll_Expression()
        {
            var data2 = new List<UserPaymentMethod>()
            {
                new UserPaymentMethod {Alias = "BBB", Id = 2},
                new UserPaymentMethod {Alias = "CCC", Id = 3}
            };

            Mock<UserPaymentMethodRepository> mock = new Mock<UserPaymentMethodRepository>(_dbContext);
            mock.Setup(r => r.GetAll(x => x.Id > 1)).Returns(data2);
            var paymentMethod = mock.Object.GetAll(x => x.Id > 1);
            Assert.AreEqual(paymentMethod, data2);
            mock.Verify();
        }
    }
}