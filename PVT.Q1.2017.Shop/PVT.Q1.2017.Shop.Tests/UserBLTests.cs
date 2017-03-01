using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Ship.Infrastructure.Repositories;
using Shop.Common.Models;
using Shop.BLL.DTO;

namespace PVT.Q1._2017.Shop.Tests
{
    [TestClass]
    public class UserBLTests
    {

        [TestMethod]
        public void RegisterUser_UserDTOInput_ReturnTrue()
        {
            Mock<IRepository<User>> repo = new Mock<IRepository<User>>();
            repo.Setup(r => r.GetAll()).Returns(new List<User>() { new User()});
            UserService service = new UserService(repo.Object);
            UserDTO user_dto = new UserDTO
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

            var registered = service.RegisterUser(user_dto);

            Assert.IsTrue(registered);
        }


        [TestMethod]
        public void RegisterUser_UserDTOInput_RealUser()
        {
            Mock<IRepository<User>> repo = new Mock<IRepository<User>>();
            repo.Setup(r => r.GetAll()).Returns(new List<User>() { new User() });
            UserService service = new UserService(repo.Object);
            UserDTO user_dto = new UserDTO
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

            var registered = service.RegisterUser(user_dto);

            Assert.IsTrue(registered);
        }
    }
}
