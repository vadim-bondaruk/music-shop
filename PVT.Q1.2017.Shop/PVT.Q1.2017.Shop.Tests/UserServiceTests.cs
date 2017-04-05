namespace PVT.Q1._2017.Shop.Tests
{
    using System;
    using global::Moq;
    using global::Shop.BLL.Services;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Linq.Expressions;
    using global::Shop.BLL.Exceptions;

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
                PhoneNumber = "+37529 123-56-78",
                Country = "Belarus"
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
            Mock.Get(_factory.GetUserRepository()).Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User { Email = email });

            UserService service = new UserService(_factory);

            var result = service.IsUserExist(email);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsUserUnique_LoginExist_ReturnTrue()
        {
            string login = "Test";
            Mock.Get(_factory.GetUserRepository()).Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User { Login = login });

            UserService service = new UserService(_factory);

            var result = service.IsUserExist(login);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsUserUnique_EmailAndLoginNotExist_ReturnFalse()
        {
            string email = "test@gmail.com";
            string login = "Test";

            Mock.Get(_factory.GetUserRepository())
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>())).Returns((User)null);

            UserService service = new UserService(_factory);

            var resultEmail = service.IsUserExist(email);
            var resultLogin = service.IsUserExist(login);

            Assert.IsFalse(resultEmail);
            Assert.IsFalse(resultLogin);

        }

        [TestMethod]
        [ExpectedException(typeof(UserValidationException))]
        public void GetEmailByUserIdentity_NullInput()
        {
            UserService service = new UserService(_factory);

            service.GetEmailByUserIdentity(null);
        }

        [TestMethod]
        public void GetEmailByUserIdentity_RealInput()
        {
            string email = "test@gmail.com";
            string login = "test";

            Mock.Get(_factory.GetUserRepository())
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new User { Email = email, Login = login });

            UserService service = new UserService(_factory);

            var resultEmail = service.GetEmailByUserIdentity(email);
            var resultLogin = service.GetEmailByUserIdentity(login);

            Assert.IsTrue(resultEmail.Equals(email, StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(resultLogin.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        [TestMethod]
        [ExpectedException(typeof(UserValidationException))]
        public void GetIdOfLogin_NullInput()
        {
            IUserService service = new UserService(_factory);

            service.GetIdOflogin(null);
        }

        [TestMethod]
        public void GetIdOfLogin_RealInput()
        {
            string email = "test@gmail.com";
            string login = "test";
            int id = 1;

            Mock.Get(_factory.GetUserRepository())
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new User { Email = email, Login = login, Id = id });

            IUserService service = new UserService(_factory);

            var loginId = service.GetIdOflogin(login);
            var emailId = service.GetIdOflogin(email);

            Assert.IsTrue(id == loginId);
            Assert.IsTrue(id == emailId);
        }


        [TestMethod]
        [ExpectedException(typeof(UserValidationException))]
        public void GetIdOfLogin_LoginAndEmailNotExist()
        {
            string email = "test@gmail.com";
            string login = "test";

            Mock.Get(_factory.GetUserRepository())
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns((User)null);

            IUserService service = new UserService(_factory);

            var loginId = service.GetIdOflogin(login);
            var emailId = service.GetIdOflogin(email);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePersonal_UserNullInput()
        {
            IUserService service = new UserService(_factory);

            service.UpdatePersonal(null, 1);
        }

        [TestMethod]
        public void UpdatePersonal_UserRealInput()
        {
            var newUser = new User
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Sex = "M",
                BirthDate = DateTime.Now,
                Country = "Belarus",
                PhoneNumber = "+37529 123-56-78"
            };

            var user = new User
            {
                FirstName = "Abziz",
                LastName = "Anand",
                Sex = "W",
                BirthDate = DateTime.Now.AddYears(-30),
                PhoneNumber = "+37529 123-56-78",
                Country = "Belarus"
            };

            Mock.Get(_factory.GetUserRepository())
               .Setup(m => m.GetById(It.IsAny<int>()))
               .Returns(user);

            IUserService service = new UserService(_factory);
            var result = service.UpdatePersonal(newUser, 1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdatePassword_NullOrEmpryPassword()
        {
            IUserService service = new UserService(_factory);

            var nullResult = service.UpdatePassword(1, null);
            var emptyResult = service.UpdatePassword(1, string.Empty);

            Assert.IsFalse(nullResult);
            Assert.IsFalse(emptyResult);
        }

        [TestMethod]
        public void UpdatePassword_RealPasswordInput()
        {
            var user = new User
            {
                Id=1,
                Password = "testuser1!",
            };
            var newPassword = "1!testuser";

            Mock.Get(_factory.GetUserRepository())
                .Setup(m => m.GetById(It.IsAny<int>()))
                .Returns(user);

            IUserService service = new UserService(_factory);

            var result = service.UpdatePassword(1, newPassword);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void UpdatePassword_NewOrOldPasswordIsNullOrEmpty()
        {
            IUserService service = new UserService(_factory);
            string password = "testpassword1!";

            var reultNewNull = service.UpdatePassword(1, null, password);
            var resultNewEmpty = service.UpdatePassword(1, string.Empty, password);
            var resultOldNull = service.UpdatePassword(1, password, null);
            var resultOldEmpty = service.UpdatePassword(1, password, string.Empty);

            Assert.IsFalse(reultNewNull);
            Assert.IsFalse(resultNewEmpty);
            Assert.IsFalse(resultOldNull);
            Assert.IsFalse(resultOldEmpty);
        }

        [TestMethod]
        [ExpectedException(typeof(UserValidationException))]
        public void UpdatePassword_PasswordNotConfirm()
        {
            var oldPassword = "testpass1!";
            var newPassword = "1!testpass";
            var currentPassword = "userpass1!";

            var user = new User
            {
                Password = currentPassword
            };

            Mock.Get(_factory.GetUserRepository())
               .Setup(m => m.GetById(It.IsAny<int>()))
               .Returns(user);

            IUserService service = new UserService(_factory);

            service.UpdatePassword(1, newPassword, oldPassword);


        }

        [TestMethod]
        [ExpectedException(typeof(UserValidationException))]
        public void UpdatePassword_PasswordConfirm()
        {
            var oldPassword = "userpass1!";
            var newPassword = "1!testpass";
            var currentPassword = "userpass1!";

            var user = new User
            {
                Password = currentPassword
            };

            Mock.Get(_factory.GetUserRepository())
               .Setup(m => m.GetById(It.IsAny<int>()))
               .Returns(user);

            IUserService service = new UserService(_factory);

            var result = service.UpdatePassword(1, newPassword, oldPassword);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void UpdateLogin_UserIdentityOrNewLoginNullOrEmptyInput()
        {
            IUserService service = new UserService(_factory);
            string newLogin = "login";
            string userIdentity = "test@mail.com";

            var reultUserNull = service.UpdateLogin(null, newLogin);
            var resultUserEmpty = service.UpdateLogin(string.Empty, newLogin);
            var resultLoginNull = service.UpdateLogin(userIdentity, null);
            var resultLoginEmpty = service.UpdateLogin(userIdentity, string.Empty);

            Assert.IsFalse(reultUserNull);
            Assert.IsFalse(resultUserEmpty);
            Assert.IsFalse(resultLoginNull);
            Assert.IsFalse(resultLoginEmpty);
        }

        [TestMethod]
        public void UpdateLogin_RealInput()
        {
            string newLogin = "login";
            string userIdentity = "test@mail.com";
            var user = new User
            {
                Login = "login89"
            };

            Mock.Get(_factory.GetUserRepository())
                 .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(user);
            IUserService service = new UserService(_factory);

            var result = service.UpdateLogin(userIdentity, newLogin);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void SoftDeleted_UserNotFound()
        {
            Mock.Get(_factory.GetUserRepository())
            .Setup(m => m.GetById(It.IsAny<int>()))
            .Returns((User)null);

            IUserService service = new UserService(_factory);

            var result = service.SoftDelete(1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SoftDeleted_UserIsFound()
        {
            Mock.Get(_factory.GetUserRepository())
                .Setup(m => m.GetById(It.IsAny<int>()))
                .Returns(new User());

            IUserService service = new UserService(_factory);

            var result = service.SoftDelete(1);

            Assert.IsTrue(result);
        }
    }
}
