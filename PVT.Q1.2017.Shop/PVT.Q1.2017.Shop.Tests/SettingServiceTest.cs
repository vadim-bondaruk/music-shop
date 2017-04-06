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
            //arrange
            int currencyId = 1;

            using (var repository = this._factory.GetSettingRepository())
            {
                //act
                repository.AddOrUpdate(new Setting { DefaultCurrencyId = currencyId});
                repository.SaveChanges();

                //assert
                Assert.IsTrue(repository.GetAll().Any());
            }
        }

        [TestMethod]
        public void GetSettingViewModelTest()
        {
            //act
            var target = _settingService.GetSettingViewModel();

            //assert
            Assert.IsInstanceOfType(target, typeof(SettingViewModel));
            Assert.IsNotInstanceOfType(target, typeof(Setting));
        }


        [TestMethod]
        public void Check_SettingViewModel_IsNotNull()
        {
            AddSettingTest();

            //act
            var result = _settingService.GetSettingViewModel();

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Check_DefaultCurrencyViewModelList_Count()
        {
            AddSettingTest();

            //act
            var result = _settingService.GetSettingViewModel();
            
            //assert
            Assert.IsTrue(result.DefaultCurrencyViewModelList.Count == 2);
        }
    }
}
