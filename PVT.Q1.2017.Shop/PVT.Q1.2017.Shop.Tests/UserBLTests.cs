﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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


namespace PVT.Q1._2017.Shop.Tests
{
    [TestClass]
    public class UserBLTests
    {

        [TestMethod]
        public void RegisterUser_UserDTOInput_ReturnTrue()
        {
            Mock<IRepositoryFactory> repo = new Mock<IRepositoryFactory>();
            repo.Setup(r => r.GetUserRepository().GetAll()).Returns(new List<User>() { new User()});
            IUserService service = new UserService(repo.Object);
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
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterUser_NullInput()
        {
            Mock<IRepositoryFactory> repo = new Mock<IRepositoryFactory>();
            repo.Setup(r => r.GetUserRepository().GetAll()).Returns(new List<User>() { new User() });
            UserService service = new UserService(repo.Object);
           
            var registered = service.RegisterUser(null);

        }


        [TestMethod]
        [ExpectedException(typeof(UserValidationException))]
        public void RegisterUser_UserDTOInput_LoginExist()
        {
            Mock<IRepositoryFactory> repo = new Mock<IRepositoryFactory>();
            repo.Setup(r => r.GetUserRepository().GetAll()).Returns(new List<User>() { new User() {Login= "OilMagnat" } });
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

        }


        [TestMethod]
        [ExpectedException(typeof(UserValidationException))]
        public void RegisterUser_UserDTOInput_EmailExist()
        {
            Mock<IRepositoryFactory> repo = new Mock<IRepositoryFactory>();
            repo.Setup(r => r.GetUserRepository().GetAll()).Returns(new List<User>() { new User() { Email = "blablabla@gmail.com" } });
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

        }
    }
}