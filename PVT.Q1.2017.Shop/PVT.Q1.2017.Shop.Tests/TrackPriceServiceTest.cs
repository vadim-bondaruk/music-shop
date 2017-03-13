using Shop.DAL.Infrastruture;

namespace PVT.Q1._2017.Shop.Tests
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using global::Moq;
    using global::Shop.BLL.Services;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    [TestClass]
    public class TrackPriceServiceTest
    {
        private readonly ITrackPriceService _trackPriceService;
        private readonly IRepositoryFactory _factory;

        public TrackPriceServiceTest()
        {
            _factory = new RepositoryFactoryMoq();
            _trackPriceService = new TrackPriceService(_factory);
        }

        [TestMethod]
        public void AddTrackPricesTest()
        {
            using (var repository = _factory.GetTrackPriceRepository())
            {
                repository.AddOrUpdate(new TrackPrice { Price = 1.99m });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Any());
            }
        }

        [TestMethod]
        public void GetTrackPriceTest()
        {
            AddTrackPricesTest();
            Assert.IsNotNull(_trackPriceService.GetTrackPrice(new Track(), new PriceLevel(), new Currency()));

            Mock.Get(_factory.GetTrackPriceRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<TrackPrice, bool>>>(),
                                     It.IsAny<Expression<Func<TrackPrice, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetTrackPriceInfoTest()
        {
            AddTrackPricesTest();
            Assert.IsNotNull(_trackPriceService.GetTrackPriceInfo(1));

            Mock.Get(_factory.GetTrackPriceRepository())
                .Verify(
                        m =>
                            m.GetById(It.IsAny<int>(),
                                      It.IsAny<Expression<Func<TrackPrice, BaseEntity>>[]>()), Times.Once);
        }
    }
}