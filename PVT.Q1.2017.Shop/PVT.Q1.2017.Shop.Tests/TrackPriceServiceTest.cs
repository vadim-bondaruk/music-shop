using Shop.DAL.Infrastruture;

namespace PVT.Q1._2017.Shop.Tests
{
    using System.Linq;
    using global::Shop.BLL;
    using global::Shop.BLL.Services;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Infrastructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;

    [TestClass]
    public class TrackPriceServiceTest
    {
        private readonly ITrackPriceService _trackPriceService;
        private readonly IRepositoryFactory _factory;

        public TrackPriceServiceTest()
        {
            IKernel kernel = new StandardKernel(new DefaultServicesNinjectModule());
            this._trackPriceService = kernel.Get<ITrackPriceService>();
            this._factory = kernel.Get<IRepositoryFactory>();

            DataBaseTest dbTest = new DataBaseTest();
            dbTest.RegisterValidTrackTest();

            CurrencyTest currencyTest = new CurrencyTest();
            currencyTest.AddValidCurrenciesTest();
        }
    }
}