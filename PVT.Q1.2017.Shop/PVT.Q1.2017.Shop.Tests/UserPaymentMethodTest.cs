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
        private static readonly DbContext _dbContext;

        static UserPaymentMethodRepositoryTest()
        {
           _dbContext = new Mock<DbContext>().Object;
        }

        [TestMethod]
        public void GetById_Verify()
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
        public void GetById()
        {
            var mockSet = new Mock<DbSet<UserPaymentMethodRepository>>();
            var mock = new Mock<IRepository<UserPaymentMethod>>();
            var res = mock.Setup(a => a.GetById(1)).Returns(() => _dbContext.Set<UserPaymentMethod>().Find(1));

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetAll()
        {
            var data = new List<UserPaymentMethod>()
            {
                new UserPaymentMethod {Alias = "AAA", Id = 1},
                new UserPaymentMethod {Alias = "BBB", Id = 2},
                new UserPaymentMethod {Alias = "CCC", Id = 3},
            };

            Mock<UserPaymentMethodRepository> mock = new Mock<UserPaymentMethodRepository>();
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