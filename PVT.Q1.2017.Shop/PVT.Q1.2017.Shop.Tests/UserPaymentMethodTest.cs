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
        [ExpectedException(typeof(NullReferenceException))]
        public void GetById_1()
        {
            //Mock<IRepository<UserPaymentMethod>> mock = new Mock<IRepository<UserPaymentMethod>>();
            //mock.Setup(r => r.GetAll()).Returns(new List<UserPaymentMethod>() {new UserPaymentMethod()});
            Mock<DbContext> mock = new Mock<DbContext>();
            mock.Setup(r => r.Set<UserPaymentMethod>());
            UserPaymentMethodRepository paymentMethodRepository = new UserPaymentMethodRepository(mock.Object);

            //UserPaymentMethod payment = new UserPaymentMethod();


            var getById = paymentMethodRepository.GetById(1);
            Assert.IsNotNull(getById);


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

            //var mock = new Mock<IRepository<UserPaymentMethod>>();
            //var res = mock.Setup(a => a.GetAll()).Returns(() => _dbContext.Set<UserPaymentMethod>().ToList());

            //Assert.IsNotNull(res);

            var data = new List<UserPaymentMethod>()
            {
                new UserPaymentMethod {Alias = "AAA", Id = 1},
                new UserPaymentMethod {Alias = "BBB", Id = 2},
                new UserPaymentMethod {Alias = "CCC", Id = 3},


            }.AsQueryable();

            var mock = new Mock<DbSet<UserPaymentMethod>>();

            mock.As<IQueryable<UserPaymentMethod>>().Setup(m => m.Provider).Returns(data.Provider);
            mock.As<IQueryable<UserPaymentMethod>>().Setup(m => m.Expression).Returns(data.Expression);
            mock.As<IQueryable<UserPaymentMethod>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock.As<IQueryable<UserPaymentMethod>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<UserPaymentMethodRepository>();
            //mockContext.Setup(m => m.GetAll()).Returns(mock.Object);

            //var service = new UserPaymentMethodRepository();


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