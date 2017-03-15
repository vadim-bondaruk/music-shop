namespace PVT.Q1._2017.Shop.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq.Expressions;

    using global::Shop.Common.Models;
    using global::Shop.DAL.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    /// <summary>
    /// </summary>
    [TestClass]
    public class UserPaymentMethodRepositoryTest
    {
        /// <summary>
        /// </summary>
        private static readonly DbContext _dbContext;

        /// <summary>
        /// Initializes static members of the <see cref="UserPaymentMethodRepositoryTest"/> class.
        /// </summary>
        static UserPaymentMethodRepositoryTest()
        {
            _dbContext = new Mock<DbContext>().Object;
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void UserPaymentMethodRepository_AddOrUpdate()
        {
            var data = new List<UserPaymentMethod>
                           {
                               new UserPaymentMethod { Alias = "AAA" },
                               new UserPaymentMethod { Alias = "BBB" },
                               new UserPaymentMethod { Alias = "CCC" }
                           };

            var mock = new Mock<UserPaymentMethodRepository>(_dbContext);
            mock.Setup(r => r.GetAll(It.IsAny<Expression<Func<UserPaymentMethod, bool>>>())).Returns(data);

            var paymentMethod = mock.Object.GetAll(It.IsAny<Expression<Func<UserPaymentMethod, bool>>>());
            Assert.AreEqual(data, paymentMethod);
            mock.Verify();
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void UserPaymentMethodRepository_GetAll()
        {
            var data = new List<UserPaymentMethod>
                           {
                               new UserPaymentMethod { Alias = "AAA" },
                               new UserPaymentMethod { Alias = "BBB" },
                               new UserPaymentMethod { Alias = "CCC" }
                           };

            var mock = new Mock<UserPaymentMethodRepository>(_dbContext);
            mock.Setup(r => r.GetAll()).Returns(data).Verifiable();

            var paymentMethod = mock.Object.GetAll();
            Assert.AreEqual(data, paymentMethod);
            mock.Verify();
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void UserPaymentMethodRepository_GetAll_Expression()
        {
            var data = new List<UserPaymentMethod>
                           {
                               new UserPaymentMethod { Alias = "AAA" },
                               new UserPaymentMethod { Alias = "BBB" },
                               new UserPaymentMethod { Alias = "CCC" }
                           };

            var mock = new Mock<UserPaymentMethodRepository>(_dbContext);
            mock.Setup(r => r.GetAll(It.IsAny<Expression<Func<UserPaymentMethod, bool>>>())).Returns(data);

            var paymentMethod = mock.Object.GetAll(It.IsAny<Expression<Func<UserPaymentMethod, bool>>>());
            Assert.AreEqual(data, paymentMethod);
            mock.Verify();
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void UserPaymentMethodRepository_GetById_Verify()
        {
            var id = 1;
            var expected = new UserPaymentMethod { Alias = "SSS" };
            var mock = new Mock<UserPaymentMethodRepository>(_dbContext);
            mock.Setup(r => r.GetById(id)).Returns(expected).Verifiable();

            var payment = mock.Object.GetById(id);
            Assert.AreEqual(expected, payment);
            mock.Verify();
        }
    }
}