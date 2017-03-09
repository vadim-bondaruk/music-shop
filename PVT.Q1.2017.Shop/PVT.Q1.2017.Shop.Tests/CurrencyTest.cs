//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Track.cs" company="PVT.Q1.2017">
//    PVT.Q1.2017
//  </copyright>
//  <summary>
//    The track.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace PVT.Q1._2017.Shop.Tests
{
    using System.Linq;

    using global::Shop.BLL;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ninject;

    /// <summary>
    /// </summary>
    [TestClass]
    public class CurrencyTest
    {
        /// <summary>
        /// </summary>
        private readonly ICurrencyService _currencyService;

        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory _factory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CurrencyTest" /> class.
        /// </summary>
        public CurrencyTest()
        {
            IKernel kernel = new StandardKernel(new DefaultServicesNinjectModule());
            this._currencyService = kernel.Get<ICurrencyService>();
            this._factory = kernel.Get<IRepositoryFactory>();
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void AddValidCurrenciesTest()
        {
            var currencyCode = 840;
            var currencyName = "USD";

            if (!this._currencyService.CurrencyExists(currencyCode))
            {
                using (var repository = this._factory.GetCurrencyRepository())
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
                using (var repository = this._factory.GetCurrencyRepository())
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
                using (var repository = this._factory.GetCurrencyRepository())
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
                using (var repository = this._factory.GetCurrencyRepository())
                {
                    repository.AddOrUpdate(new Currency { ShortName = currencyName, Code = currencyCode });
                    repository.SaveChanges();
                }

                Assert.IsTrue(this._currencyService.CurrencyExists(currencyCode));
                Assert.IsTrue(this._currencyService.CurrencyExists(currencyName));
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetCurrenciesListTest()
        {
            this.AddValidCurrenciesTest();
            Assert.IsTrue(this._currencyService.GetCurrenciesList().Any());
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetCurrencyByCodeTest()
        {
            this.AddValidCurrenciesTest();
            Assert.IsNotNull(this._currencyService.GetCurrencyByCode(840));
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetCurrencyByNameTest()
        {
            this.AddValidCurrenciesTest();
            Assert.IsNotNull(this._currencyService.GetCurrencyByName("byn"));
        }
    }
}