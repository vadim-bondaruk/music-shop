namespace PVT.Q1._2017.Shop.Tests
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using global::Shop.BLL.Services;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Moq;

    [TestClass]
    public class CurrencyServiceTest
    {
        private readonly ICurrencyService _currencyService;
        private readonly IRepositoryFactory _factory;

        public CurrencyServiceTest()
        {
            _factory = new RepositoryFactoryMoq();
            _currencyService = new CurrencyService(_factory);
        }

        [TestMethod]
        public void AddCurrenciesTest()
        {
            int currencyCode = 840;
            string currencyName = "USD";

            using (var repository = _factory.GetCurrencyRepository())
            {
                repository.AddOrUpdate(new Currency { ShortName = currencyName, Code = currencyCode });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Any());
            }
        }

        [TestMethod]
        public void GetCurrenciesListTest()
        {
            using (var repository = _factory.GetCurrencyRepository())
            {
                repository.AddOrUpdate(new Currency { ShortName = "EUR", Code = 978 });
                repository.SaveChanges();
            }

            Assert.IsTrue(_currencyService.GetCurrenciesList().Any());

            Mock.Get(_factory.GetCurrencyRepository()).Verify(m => m.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetCurrencyByCodeTest()
        {
            AddCurrenciesTest();
            Assert.IsNotNull(_currencyService.GetCurrencyByCode(840));

            Mock.Get(_factory.GetCurrencyRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Currency, bool>>>(),
                                     It.IsAny<Expression<Func<Currency, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetCurrencyByNameTest()
        {
            AddCurrenciesTest();
            Assert.IsNotNull(_currencyService.GetCurrencyByName("byn"));

            Mock.Get(_factory.GetCurrencyRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Currency, bool>>>(),
                                     It.IsAny<Expression<Func<Currency, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void CurrencyExistsTest()
        {
            AddCurrenciesTest();
            Assert.IsTrue(_currencyService.CurrencyExists(new Currency()));

            Mock.Get(_factory.GetCurrencyRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Currency, bool>>>(),
                                     It.IsAny<Expression<Func<Currency, BaseEntity>>[]>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void GetCurrencyInfoTest()
        {
            AddCurrenciesTest();
            Assert.IsNotNull(_currencyService.GetCurrencyInfo(1));

            Mock.Get(_factory.GetCurrencyRepository())
                .Verify(
                        m =>
                            m.GetById(It.IsAny<int>(),
                                      It.IsAny<Expression<Func<Currency, BaseEntity>>[]>()), Times.Once);
        }
    }
}