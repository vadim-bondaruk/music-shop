using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.BLL.Services.Infrastructure;
using Shop.DAL.Infrastruture;
using PVT.Q1._2017.Shop.Tests.Moq;
using Shop.BLL.Services;
using Shop.Common.Models;
using System.Linq;
using Moq;
using Shop.Common.ViewModels;

namespace PVT.Q1._2017.Shop.Tests
{
    /// <summary>
    /// Summary description for SettingServiceTest
    /// </summary>
    [TestClass]
    public class SettingServiceTest
    {
        private readonly ISettingService _settingService;
        private readonly IRepositoryFactory _factory;

        public SettingServiceTest()
        {
            _factory = new RepositoryFactoryMoq();
            _settingService = new SettingService(_factory);
        }

        [TestMethod]
        public void AddSettingTest()
        {
            int currencyId = 1;
            int priceLevelId = 1;

            using (var repository = this._factory.GetSettingRepository())
            {
                repository.AddOrUpdate(new Setting { DefaultCurrencyId = currencyId, DefaultPriceLevelId = priceLevelId });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Any());
            }
        }

        [TestMethod]
        public void GetSettingViewModelTest()
        {
            this.AddSettingTest();

        }


        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
        }
    }
}
