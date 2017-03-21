using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Shop.Infrastructure.Repositories;
using Shop.Common.Models;
using Shop.BLL.DTO;
using Shop.BLL.Exceptions;
using Shop.BLL.Services;
using Shop.BLL.Services.Infrastructure;
using Shop.DAL.Infrastruture;
using PVT.Q1._2017.Shop.ViewModels;

namespace PVT.Q1._2017.Shop.Tests
{
    [TestClass]
    public class UserServiceTests
    {

        [TestMethod]
        public void RegisterUser_UserInput_ReturnTrue()
        {
            Mock<IUserRepository> repo = new Mock<IUserRepository>();
            repo.Setup(r => r.GetAll()).Returns(new List<User>() { new User()});
            IUserService service = new UserService(repo.Object);
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
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterUser_NullInput()
        {
            Mock<IUserRepository> repo = new Mock<IUserRepository>();
            repo.Setup(r =>r.GetAll()).Returns(new List<User>() { new User() });
            UserService service = new UserService(repo.Object);
           
            var registered = service.RegisterUser(null);

        }
    }
}
