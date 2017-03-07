namespace PVT.Q1._2017.Shop.Tests
{
    using System.Linq;
    using global::Shop.BLL;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;

    [TestClass]
    public class CurrencyTest
    {
        private readonly ICurrencyService _currencyService;
        private readonly IRepositoryFactory _factory;

        public CurrencyTest()
        {
            IKernel kernel = new StandardKernel(new DefaultServicesNinjectModule());
            this._currencyService = kernel.Get<ICurrencyService>();
            this._factory = kernel.Get<IRepositoryFactory>();
        }

        [TestMethod]
        public void AddValidCurrenciesTest()
        {
            int currencyCode = 840;
            string currencyName = "USD";

            if (!this._currencyService.CurrencyExists(currencyCode))
            {
                using (var repository = this._factory.CreateCurrencyRepository())
                {
                    repository.AddOrUpdate(new Currency { ShortName = currencyName, Code = currencyCode });
                    repository.SaveChanges();
                }
                    
                Assert.IsTrue(this._currencyService.CurrencyExists(currencyCode));
                Assert.IsTrue(this._currencyService.CurrencyExists(currencyName));
            }

            currencyCode = 978;
            currencyName = "EUR";

            if (!this._currencyService.CurrencyExists(currencyCode))
            {
                using (var repository = this._factory.CreateCurrencyRepository())
                {
                    repository.AddOrUpdate(new Currency { ShortName = currencyName, Code = currencyCode });
                    repository.SaveChanges();
                }

                Assert.IsTrue(this._currencyService.CurrencyExists(currencyCode));
                Assert.IsTrue(this._currencyService.CurrencyExists(currencyName));
            }

            currencyCode = 810; // <- this code is still using in finance operations!
            currencyName = "RUB";

            if (!this._currencyService.CurrencyExists(currencyCode))
            {
                using (var repository = this._factory.CreateCurrencyRepository())
                {
                    repository.AddOrUpdate(new Currency { ShortName = currencyName, Code = currencyCode });
                    repository.SaveChanges();
                }

                Assert.IsTrue(this._currencyService.CurrencyExists(currencyCode));
                Assert.IsTrue(this._currencyService.CurrencyExists(currencyName));
            }

            currencyCode = 933; // <- this code is still using in finance operations!
            currencyName = "BYN";

            if (!this._currencyService.CurrencyExists(currencyCode))
            {
                using (var repository = this._factory.CreateCurrencyRepository())
                {
                    repository.AddOrUpdate(new Currency { ShortName = currencyName, Code = currencyCode });
                    repository.SaveChanges();
                }

                Assert.IsTrue(this._currencyService.CurrencyExists(currencyCode));
                Assert.IsTrue(this._currencyService.CurrencyExists(currencyName));
            }
        }

        [TestMethod]
        public void GetCurrenciesListTest()
        {
            this.AddValidCurrenciesTest();
            Assert.IsTrue(this._currencyService.GetCurrenciesList().Any());
        }

        [TestMethod]
        public void GetCurrencyByCodeTest()
        {
            this.AddValidCurrenciesTest();
            Assert.IsNotNull(this._currencyService.GetCurrencyByCode(840));
        }

        [TestMethod]
        public void GetCurrencyByNameTest()
        {
            this.AddValidCurrenciesTest();
            Assert.IsNotNull(this._currencyService.GetCurrencyByName("byn"));
        }
    }
}