using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shop.DAL.Infrastruture;
using Shop.BLL.Services.Infrastructure;
using System.Collections.Generic;
using Shop.Common.Models;
using PVT.Q1._2017.Shop.Areas.Admin.Controllers;
using System.Web.Mvc;
using PVT.Q1._2017.Shop.Tests.Moq;

namespace PVT.Q1._2017.Shop.Tests
{
    using global::Shop.Common.ViewModels;

    [TestClass]
    public class AdminCurrencyControllerTests
    {
        [TestMethod]
        public void Index_Contains_All_Currencies()
        {
            // Arrange - create the mock service
            var currencyRepositoryMoq = new Mock<ICurrencyRepository>();
            var mockService = new Mock<ICurrencyService>();
            mockService.Setup(x => x.GetCurrenciesList()).Returns(new List<CurrencyViewModel>()
            {
                new CurrencyViewModel {ShortName = "USD", Code = 840},
                new CurrencyViewModel {ShortName = "EUR", Code = 978}
            });

            var controller = new CurrencyController(currencyRepositoryMoq.Object, mockService.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(List<CurrencyViewModel>));
            Assert.IsTrue(((List<CurrencyViewModel>)result.Model).Count == 3);
        }

        [TestMethod]
        public void Can_Edit_Currency()
        {
            // Arrange - create the mock service
            var currencyRepositoryMoq = new CurrencyRepositoryMoq();
            var mockService = new Mock<ICurrencyService>();

            // Arrange 
            var controller = new CurrencyController(currencyRepositoryMoq.Repository, mockService.Object);

            // Act
            var result = controller.Edit(1);

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(CurrencyViewModel));
        }

        [TestMethod]
        public void Can_Edit_Nonexistent_Currency()
        {
            // Arrange - create the mock service
            var currencyRepositoryMoq = new Mock<ICurrencyRepository>();
            var mockService = new Mock<ICurrencyService>();

            // Arrange - create the controller
            var controller = new CurrencyController(currencyRepositoryMoq.Object, mockService.Object);

            // Act
            var result = controller.Edit(4);

            // Assert
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void Can_Delete_Valid_Currencies()
        {
            // Arrange - create a Currency
            var curr = new Currency { Id = 1, ShortName = "USD" };

            // Arrange - create the mock repository
            var mockRepository = new Mock<ICurrencyRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Currency>()
            {
                new Currency { Id = 1, ShortName = "USD" },
                new Currency { Id = 2, ShortName = "Euro" },
                new Currency { Id = 3, ShortName = "BYR" }
            });

            // Arrange - create the mock service
            var mockService = new Mock<ICurrencyService>();

            // Arrange - create the controller
            var target = new CurrencyController(mockRepository.Object, mockService.Object);

            // Act - delete the service
            var result = target.Delete(curr.Id);

            // Assert
            mockRepository.Verify(x => x.Delete(curr.Id));
            Assert.AreEqual("Index", ((RedirectToRouteResult)result).RouteValues["action"]);
        }
    }
}
