namespace PVT.Q1._2017.Shop.Tests
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using global::Moq;

    using global::Shop.BLL.Services;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PVT.Q1._2017.Shop.Tests.Moq;

    /// <summary>
    ///     Summary description for TrackServiceTest
    /// </summary>
    [TestClass]
    public class TrackServiceTest
    {
        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory factory;

        /// <summary>
        /// </summary>
        private readonly ITrackService trackService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackServiceTest"/> class.
        /// </summary>
        public TrackServiceTest()
        {
            //this.factory = new RepositoryFactoryMoq();
            //this.trackService = new TrackService(this.factory);

            //using (var repository = this.factory.GetCurrencyRepository())
            //{
            //    repository.AddOrUpdate(new Currency { Id = 1, ShortName = "USD", Code = 840 });
            //    repository.SaveChanges();
            //}

            //using (var repository = this.factory.GetPriceLevelRepository())
            //{
            //    repository.AddOrUpdate(new PriceLevel { Id = 1, Name = "Default Price Level" });
            //    repository.SaveChanges();
            //}
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void AddTrackTest()
        {
            using (var repository = this.factory.GetTrackRepository())
            {
                repository.AddOrUpdate(new Track { Name = "Some track" });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Any());
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetAlbumsList()
        {
            this.AddTrackTest();

            var track = this.trackService.GetTracksList().FirstOrDefault();
            Assert.IsNotNull(track);

            using (var repository = this.factory.GetAlbumRepository())
            {
                repository.AddOrUpdate(new Album { ArtistId = 1, Name = "Some album" });
                repository.SaveChanges();
            }

            using (var repository = this.factory.GetAlbumTrackRelationRepository())
            {
                repository.AddOrUpdate(new AlbumTrackRelation { TrackId = 1, AlbumId = 2 });
            }

            Assert.IsTrue(this.trackService.GetAlbumsList(track.Id, 840, 1).Albums.Any());

            Mock.Get(this.factory.GetAlbumRepository())
                .Verify(m => m.GetAll(It.IsAny<Expression<Func<Album, bool>>>()), Times.Once);
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetTrackDetailsTest()
        {
            this.AddTrackTest();
            Assert.IsNotNull(this.trackService.GetTrackDetails(1));
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetTracksListTest()
        {
            this.AddTrackTest();
            Assert.IsTrue(this.trackService.GetTracksList().Any());
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetTracksWithoutPriceTest()
        {
            this.AddTrackTest();
            Assert.IsTrue(this.trackService.GetTracksWithoutPrice().Any());
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetTracksWithPriceTest()
        {
            this.AddTrackTest();

            var track = this.trackService.GetTracksList().FirstOrDefault();
            Assert.IsNotNull(track);

            track.Price = new PriceViewModel
                              {
                                  Amount = 1.99m,
                                  Currency = new CurrencyViewModel { Code = 840, ShortName = "USD" }
                              };

            Assert.IsNotNull(this.trackService.GetTracksWithPrice());
        }
    }
}