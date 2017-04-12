namespace PVT.Q1._2017.Shop.Tests
{
    using System;
    using System.Collections.Generic;
    using global::Moq;
    using global::Shop.BLL.Services;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Linq.Expressions;

    [TestClass]
    public class UserServiceTests
    {
        private readonly IRepositoryFactory _factory;

        public UserServiceTests()
        {
            _factory = new RepositoryFactoryMoq();

            using (var repository = this._factory.GetCurrencyRepository())
            {
                repository.AddOrUpdate(new Currency { Id = 1, ShortName = "USD", Code = 840 });
                repository.SaveChanges();
            }

            using (var repository = this._factory.GetPriceLevelRepository())
            {
                repository.AddOrUpdate(new PriceLevel { Id = 1, Name = "Default Price Level" });
                repository.SaveChanges();
            }
        }

        [TestMethod]
        public void RegisterUser_UserInput_ReturnTrue()
        {
            IUserService service = new UserService(_factory);
            User user = new User
            {
                FirstName = "Abziz",
                LastName = "Anand",
                Login = "OilMagnat",
                Sex = "W",
                Email = "blablabla@gmail.com",
                Password = "12345",
                BirthDate = DateTime.Now.AddYears(-30),
                PhoneNumber="+37529 123-56-78",
                Country="Belarus"
            };

            var registered = service.RegisterUser(user);
            Assert.IsTrue(registered);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void RegisterUser_NullInput()
        {
            UserService service = new UserService(_factory);
            service.RegisterUser(null);
        }

        [TestMethod]
        public void IsUserUnique_NullOrEmptyInput()
        {
            UserService service = new UserService(_factory);

            var resultNull = service.IsUserExist(null);
            var resultEmpty = service.IsUserExist("");

            Assert.IsFalse(resultNull);
            Assert.IsFalse(resultEmpty);
        }

        [TestMethod]
        public void IsUserUnique_EmailExist_ReturnTrue()
        {
            string email = "test@gmail.com";
            Mock.Get(_factory.GetUserRepository()).Setup(m => m.Exist(It.IsAny<Expression<Func<User, bool>>>())).Returns(true);

            UserService service = new UserService(_factory);

            var result = service.IsUserExist(email);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsUserUnique_LoginExist_ReturnTrue()
        {
            string login = "Test";
            Mock.Get(_factory.GetUserRepository()).Setup(m => m.Exist(It.IsAny<Expression<Func<User, bool>>>())).Returns(true);

            UserService service = new UserService(_factory);

            var result = service.IsUserExist(login);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsUserUnique_EmailAndLoginNotExist_ReturnFalse()
        {
            string email = "test@gmail.com";
            string login = "Test";   

            Mock.Get(_factory.GetUserRepository()).Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>())).Returns((User)null);
           
            UserService service = new UserService(_factory);

            var resultEmail = service.IsUserExist(email);
            var resultLogin = service.IsUserExist(login);

            Assert.IsFalse(resultEmail);
            Assert.IsFalse(resultLogin);
        
        }
    }
}
