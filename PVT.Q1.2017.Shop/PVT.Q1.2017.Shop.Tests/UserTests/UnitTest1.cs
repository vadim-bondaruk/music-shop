namespace PVT.Q1._2017.Shop.Tests.UserTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Ship.Infrastructure.Repositories;
    using global::Shop.Infrastructure.Enums;
    using global::Shop.Common.Models;

    [TestClass]
    public class UnitTest1
    {
        #region Initial test data - in _users variable
        List<User> _users = new List<User>
            {
                new User
                {
                Id=0,
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
                Id=1,
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
                Id=2,
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
                Id=3,
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

        Mock<IRepository<User>> mock = new Mock<IRepository<User>>();

        [TestMethod]
        public void GetById_Real_Input()
        {
            var expected = _users.Find(t => t.Id == 2);
            mock.Setup(i => i.GetById(2)).Returns(expected);

            var actual = mock.Object.GetById(2);

            Assert.IsTrue(actual.Id == 2);
            Assert.IsInstanceOfType(actual, typeof(User));
        }

        [TestMethod]
        public void GetAllUsers_Test()
        {
            mock.Setup(i => i.GetAll()).Returns(_users);
            var actual = mock.Object.GetAll();
            Assert.AreEqual(_users, actual);
        }


        [TestMethod]
        public void AddOrUpdate_Model_Is_Null()
        {
            mock.Setup(i => i.AddOrUpdate(null)).Throws(new ArgumentNullException());
            mock.Verify();
        }

        [TestMethod]
        public void Delete_byId()
        {
            mock.Setup(i => i.Delete(2));

            mock.Object.Delete(2);
            var a = mock.Object.GetById(2);
            Assert.IsTrue(a == null);

            // Assert that the Delete method was called once
            mock.Verify(x => x.Delete(2), Times.Once);
        }
    }
}
