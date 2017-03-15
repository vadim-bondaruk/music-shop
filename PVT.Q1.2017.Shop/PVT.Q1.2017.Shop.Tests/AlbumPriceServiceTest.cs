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

    using Moq;

    using PVT.Q1._2017.Shop.Tests.Mocks;

    /// <summary>
    /// </summary>
    [TestClass]
    public class AlbumPriceServiceTest
    {
        /// <summary>
        /// </summary>
        private readonly IAlbumPriceService _albumPriceService;

        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumPriceServiceTest"/> class.
        /// </summary>
        public AlbumPriceServiceTest()
        {
            this._factory = new RepositoryFactoryMoq();
            this._albumPriceService = new AlbumPriceService(this._factory);
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void AddAlbumPricesTest()
        {
            using (var repository = this._factory.GetAlbumPriceRepository())
            {
                repository.AddOrUpdate(new AlbumPrice { Price = 9.99m });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Any());
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetAlbumPriceTest()
        {
            this.AddAlbumPricesTest();
            Assert.IsNotNull(this._albumPriceService.GeAlbumPrice(new Album(), new PriceLevel(), new Currency()));

            Mock.Get(this._factory.GetAlbumPriceRepository())
                .Verify(
                    m =>
                        m.GetAll(
                            It.IsAny<Expression<Func<AlbumPrice, bool>>>(),
                            It.IsAny<Expression<Func<AlbumPrice, BaseEntity>>[]>()),
                    Times.Once);
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetTrackPriceInfoTest()
        {
            this.AddAlbumPricesTest();
            Assert.IsNotNull(this._albumPriceService.GetAlbumPriceInfo(1));

            Mock.Get(this._factory.GetAlbumPriceRepository())
                .Verify(
                    m => m.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<AlbumPrice, BaseEntity>>[]>()),
                    Times.Once);
        }
    }
}