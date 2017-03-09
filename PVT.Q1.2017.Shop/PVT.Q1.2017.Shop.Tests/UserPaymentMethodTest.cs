using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shop.Common.Models;
using Shop.DAL.Repositories;
using Shop.Infrastructure.Repositories;


namespace PVT.Q1._2017.Shop.Tests
{
    [TestClass]
    public class UserPaymentMethodRepositoryTest
    {
        private readonly DbContext _dbContext;

        [TestMethod]
        public void GetById_Verify()
        {
            Mock<DbContext> dbContext = new Mock<DbContext>();
            var id = 1;
            var expected = new UserPaymentMethod() { Alias = "SSS"};
            Mock<UserPaymentMethodRepository> mock = new Mock<UserPaymentMethodRepository>(dbContext.Object);
            mock.Setup(r => r.GetById(id)).Returns(expected).Verifiable();

            UserPaymentMethod payment = mock.Object.GetById(id);
            Assert.AreEqual(expected, payment);
            mock.Verify();
        }


        [TestMethod]
        public void GetAll()
        {
            Mock<DbContext> dbContext = new Mock<DbContext>();

            var data = new List<UserPaymentMethod>()
            {
                new UserPaymentMethod {Alias = "AAA"},
                new UserPaymentMethod {Alias = "BBB"},
                new UserPaymentMethod {Alias = "CCC"},
            };

            Mock<UserPaymentMethodRepository> mock = new Mock<UserPaymentMethodRepository>(dbContext.Object);
            mock.Setup(r => r.GetAll()).Returns(data).Verifiable();

            ICollection<UserPaymentMethod> paymentMethod = mock.Object.GetAll();
            Assert.AreEqual(data, paymentMethod);
            mock.Verify();
        }



        [TestMethod]
        public void AddOrUpdate()
        {
            var model = new UserPaymentMethod();
            var mock = new Mock<IRepository<UserPaymentMethod>>();
            var res = mock.Setup(a => a.AddOrUpdate(model));

            Assert.IsNotNull(res);

        }


        [TestMethod]
        public void Delete_model()
        {
            var model = new UserPaymentMethod();

            var mock = new Mock<IRepository<UserPaymentMethod>>();
            var res = mock.Setup(a => a.Delete(model));

            Assert.IsNotNull(res);

        }

        [TestMethod]
        public void Delete_id()
        {
            var model = new UserPaymentMethod();

            var mock = new Mock<IRepository<UserPaymentMethod>>();
            var res = mock.Setup(a => a.Delete(model.Id));

            Assert.IsNotNull(res);

        }

        [TestMethod]
        public void GetAll_Expression()
        {
            var model = new UserPaymentMethod();

            var mock = new Mock<IRepository<UserPaymentMethod>>();
            var res = mock.Setup(a => a.GetAll(b => b.Id < 0)).Returns(() => _dbContext.Set<UserPaymentMethod>().ToList());

            Assert.IsNotNull(res);

        }
    }
}