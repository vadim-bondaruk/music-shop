using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shop.DAL.Infrastruture;
using Shop.BLL.Services.Infrastructure;
using System.Collections.Generic;
using Shop.Common.Models;
using PVT.Q1._2017.Shop.Areas.Admin.Controllers;

namespace PVT.Q1._2017.Shop.Tests
{
    [TestClass]
    public class AdminCurrencyControllerTests
    {
        [TestMethod]
        public void Index_Contains_All_Currencies()
        {
            // Arrange - create the mock service
            var mockService = new Mock<ICurrencyService>();
            mockService.Setup(x => x.GetCurrenciesList()).Returns(new List<Currency>()
            {
                new Currency {Id = 1, ShortName = "USD" },
                new Currency {Id = 2, ShortName = "Euro" },
                new Currency {Id = 3 , ShortName = "BYR" }
            });

            //Act
            var result = mockService.Object.GetCurrenciesList();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 3);
        }

        [TestMethod]
        public void Can_Edit_Currency()
        {
            //Arrange - create the mock service
            var mockService = new Mock<ICurrencyService>();
            mockService.Setup(x => x.GetCurrenciesList()).Returns(new List<Currency>()
            {
                new Currency {Id = 1, ShortName = "USD" },
                new Currency {Id = 2, ShortName = "Euro" },
                new Currency {Id = 3 , ShortName = "BYR" }
            });

            var mockRepository = new Mock<ICurrencyRepository>();

            //Arrange - create the controller
            var target = new CurrencyController(mockRepository.Object, mockService.Object);

            //Act
            var c1 = target.Edit(1).ViewData.Model as Currency;
            var c2 = target.Edit(2).ViewData.Model as Currency;
            var c3 = target.Edit(3).ViewData.Model as Currency;

            //Assert
            Assert.AreEqual(1, c1.Id);
            Assert.AreEqual(2, c2.Id);
            Assert.AreEqual(3, c3.Id);
        }

        [TestMethod]
        public void Can_Edit_Nonexistent_Currency()
        {
            //Arrange - create the mock service
            var mockService = new Mock<ICurrencyService>();
            mockService.Setup(x => x.GetCurrenciesList()).Returns(new List<Currency>()
            {
                new Currency {Id = 1, ShortName = "USD" },
                new Currency {Id = 2, ShortName = "Euro" },
                new Currency {Id = 3 , ShortName = "BYR" }
            });

            var mockRepository = new Mock<ICurrencyRepository>();

            //Arrange - create the controller
            CurrencyController target = new CurrencyController(mockRepository.Object, mockService.Object);

            //Act
            Currency result = (Currency)target.Edit(4).ViewData.Model;

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Delete_Valid_Currencies()
        {
            //Arrange - create a Currency
            var curr = new Currency { Id = 1, ShortName = "USD" };
            //Arrange - create the mock repository
            var mockRepository = new Mock<ICurrencyRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Currency>()
            {
                new Currency {Id = 1, ShortName = "USD" },
                new Currency {Id = 2, ShortName = "Euro" },
                new Currency {Id = 3 , ShortName = "BYR" }
            });

            //Arrange - create the mock service
            var mockService = new Mock<ICurrencyService>();

            //Arrange - create the controller
            var target = new CurrencyController(mockRepository.Object, mockService.Object);

            //Act - delete the service
            target.Delete(curr.Id);

            //Assert
            mockRepository.Verify(x => x.Delete(curr.Id));
        }
    }
}
