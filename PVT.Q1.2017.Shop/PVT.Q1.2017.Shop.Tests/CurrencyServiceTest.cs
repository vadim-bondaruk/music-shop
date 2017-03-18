namespace PVT.Q1._2017.Shop.Tests
{
    using System.Linq;
    using global::Shop.BLL.Services;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class CurrencyServiceTest
    {
        private readonly ICurrencyService _currencyService;
        private readonly IRepositoryFactory _factory;

        public CurrencyServiceTest()
        {
            this._factory = new RepositoryFactoryMoq();
            this._currencyService = new CurrencyService(this._factory);
        }

        [TestMethod]
        public void AddCurrenciesTest()
        {
            int currencyCode = 840;
            string currencyName = "USD";

            using (var repository = this._factory.GetCurrencyRepository())
            {
                repository.AddOrUpdate(new Currency { ShortName = currencyName, Code = currencyCode });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Any());
            }
        }

        [TestMethod]
        public void GetCurrenciesListTest()
        {
            Assert.IsFalse(this._currencyService.GetCurrenciesList().Any());
            this.AddCurrenciesTest();
            Assert.IsTrue(this._currencyService.GetCurrenciesList().Any());
        }

        [TestMethod]
        public void GetCurrencyByCodeTest()
        {
            this.AddCurrenciesTest();
            Assert.IsNotNull(this._currencyService.GetCurrencyByCode(840));
        }

        [TestMethod]
        public void GetCurrencyByNameTest()
        {
            this.AddCurrenciesTest();
            Assert.IsNotNull(this._currencyService.GetCurrencyByName("byn"));
        }

        [TestMethod]
        public void GetCurrencyInfoTest()
        {
            this.AddCurrenciesTest();
            Assert.IsNotNull(this._currencyService.GetCurrency(1));
        }
    }
}