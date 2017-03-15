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
    using Moq;
    using Ninject;

    [TestClass]
    public class TrackPriceServiceTest
    {
        #region Fields

        private readonly ITrackPriceService _trackPriceService;
        private readonly IRepositoryFactory _factory;

        #endregion //Fields

        #region Constructors

        public TrackPriceServiceTest()
        {
            this._factory = new RepositoryFactoryMoq();
            this._trackPriceService = new TrackPriceService(this._factory);
        }

        #endregion //Constructors

        #region Tests

        [TestMethod]
        public void AddTrackPriceTest()
        {
            using (var repository = this._factory.GetTrackPriceRepository())
            {
                repository.AddOrUpdate(new TrackPrice { Price = 1.99m });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Any());
            }
        }

        [TestMethod]
        public void GetTrackPriceTest()
        {
            AddTrackPriceTest();
            Assert.IsNotNull(this._trackPriceService.GetTrackPrice(new Track(), new PriceLevel(), new Currency()));
        }

        [TestMethod]
        public void GetTrackPriceInfoTest()
        {
            AddTrackPriceTest();
            Assert.IsNotNull(this._trackPriceService.GetTrackPrice(1));
        }

        #endregion //Tests
    }
}