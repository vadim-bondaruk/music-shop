namespace PVT.Q1._2017.Shop.Tests.UserTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Ship.Infrastructure.Repositories;
    using global::Shop.Common.Models;
    using global::Shop.Infrastructure.Enums;

    /// <summary>
    /// TestClass
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        #region Initial test data - in _users variable
        /// <summary>
        /// Initial test data
        /// </summary>
        private List<User> users = new List<User>
            {
                new User
                {
                Id = 0,
                FirstName = "John",
                LastName = "Gates",
                Login = "kasper",
                Sex = "M",
                UserRole = UserRoles.User,
                Email = "KasperKasper@gmail.com",
                Password = "СлаваКПСС!11"
                },

                new User
                {
                Id = 1,
                FirstName = "Alice",
                LastName = "McNeal",
                Login = "alisochka",
                Sex = "W",
                UserRole = UserRoles.User,
                Email = "alice1@gmail.com",
                Password = "100VK500"
                },

                new User
                {
                Id = 2,
                FirstName = "Artur",
                LastName = "Li",
                Login = "arturio",
                Sex = "M",
                UserRole = UserRoles.Admin,
                Email = "artII@mail.ru",
                Password = "nobodyKnowMyP-d1"
                },

                new User
                {
                Id = 3,
                FirstName = "Abziz",
                LastName = "Anand",
                Login = "OilMagnat",
                Sex = "M",
                UserRole = UserRoles.VIPUser,
                Email = "blablabla@gmail.com",
                Password = "12345"
                }
            };
        #endregion

        /// <summary>
        /// MOQ object
        /// </summary>
        private Mock<IRepository<User>> mock = new Mock<IRepository<User>>();

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetById_Real_Input()
        {
            var expected = users.Find(t => t.Id == 2);
            this.mock.Setup(i => i.GetById(2)).Returns(expected);

            var actual = this.mock.Object.GetById(2);

            Assert.IsTrue(actual.Id == 2);
            Assert.IsInstanceOfType(actual, typeof(User));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetAllUsers_Test()
        {
            this.mock.Setup(i => i.GetAll()).Returns(this.users);
            var actual = this.mock.Object.GetAll();
            Assert.AreEqual(this.users, actual);
        }
        
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddOrUpdate_Model_Is_Null()
        {
            this.mock.Setup(i => i.AddOrUpdate(null)).Throws(new ArgumentNullException());
            this.mock.Verify();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Delete_byId()
        {
            this.mock.Setup(i => i.Delete(2));

            this.mock.Object.Delete(2);
            var a = this.mock.Object.GetById(2);
            Assert.IsTrue(a == null);

            // Assert that the Delete method was called once
            this.mock.Verify(x => x.Delete(2), Times.Once);
        }
    }
}
