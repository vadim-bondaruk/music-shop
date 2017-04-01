namespace PVT.Q1._2017.Shop.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using global::Shop.BLL.Services;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using global::Moq;

    [TestClass]
    public class ArtistServiceTest
    {
        private readonly IArtistService _artistService;
        private readonly IRepositoryFactory _factory;

        public ArtistServiceTest()
        {
            _factory = new RepositoryFactoryMoq();
            _artistService = new ArtistService(_factory);
        }

        [TestMethod]
        public void AddArtistTest()
        {
            using (var repository = _factory.GetArtistRepository())
            {
                repository.AddOrUpdate(new Artist { Name = "Some artist" });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Any());
            }
        }

        [TestMethod]
        public void GetArtistDetailsTest()
        {
            AddArtistTest();
            Assert.IsNotNull(_artistService.GetArtistDetails(1));

            Mock.Get(_factory.GetArtistRepository())
                .Verify(m => m.GetById(It.IsAny<int>()), Times.Once);

            Mock.Get(_factory.GetTrackRepository())
                .Verify(m => m.Count(It.IsAny<Expression<Func<Track, bool>>>()), Times.Once);

            Mock.Get(_factory.GetAlbumRepository())
                .Verify(m => m.Count(It.IsAny<Expression<Func<Album, bool>>>()), Times.Once);
        }

        [TestMethod]
        public void GetTracksListTest()
        {
            AddArtistTest();

            using (var repository = _factory.GetTrackRepository())
            {
                repository.AddOrUpdate(new Track { Name = "Some track" });
                repository.SaveChanges();
            }

            Assert.IsNotNull(_artistService.GetTracksList(1));

            Mock.Get(_factory.GetArtistRepository())
                .Verify(m => m.GetById(It.IsAny<int>()), Times.Once);

            Mock.Get(_factory.GetTrackRepository())
                .Verify(m => m.GetAll(It.IsAny<Expression<Func<Track, bool>>>()), Times.Once);
        }

        [TestMethod]
        public void GetAlbumsListTest()
        {
            AddArtistTest();

            using (var repository = _factory.GetAlbumRepository())
            {
                repository.AddOrUpdate(new Album { Name = "Some album" });
                repository.SaveChanges();
            }

            Assert.IsNotNull(_artistService.GetAlbumsList(1));

            Mock.Get(_factory.GetArtistRepository())
                .Verify(m => m.GetById(It.IsAny<int>()), Times.Once);

            Mock.Get(_factory.GetAlbumRepository())
                .Verify(m => m.GetAll(It.IsAny<Expression<Func<Album, bool>>>()), Times.Once);
        }

        [TestMethod]
        public void GetTracksWithoutPriceTest()
        {
            AddArtistTest();

            using (var repository = _factory.GetTrackRepository())
            {
                repository.AddOrUpdate(new Track { Name = "Some track" });
                repository.SaveChanges();
            }

            Assert.IsNotNull(_artistService.GetTracksWithoutPrice(1));

            Mock.Get(_factory.GetArtistRepository())
                .Verify(m => m.GetById(It.IsAny<int>()), Times.Once);

            Mock.Get(_factory.GetTrackRepository())
                .Verify(m => m.GetAll(It.IsAny<Expression<Func<Track, bool>>>()), Times.Once);
        }

        [TestMethod]
        public void GetTracksWithPriceTest()
        {
            AddArtistTest();

            using (var repository = _factory.GetTrackRepository())
            {
                repository.AddOrUpdate(new Track
                {
                    Name = "Some track",
                    TrackPrices = new List<TrackPrice> { new TrackPrice { Price = 1.99m } }
                });
                repository.SaveChanges();
            }

            Assert.IsNotNull(_artistService.GetTracksWithPrice(1));

            Mock.Get(_factory.GetArtistRepository())
                .Verify(m => m.GetById(It.IsAny<int>()), Times.Once);

            Mock.Get(_factory.GetTrackRepository())
                .Verify(m => m.GetAll(It.IsAny<Expression<Func<Track, bool>>>()), Times.Once);

            Mock.Get(_factory.GetTrackPriceRepository())
                .Verify(m => m.FirstOrDefault(It.IsAny<Expression<Func<TrackPrice, bool>>>(),
                                              It.IsAny<Expression<Func<TrackPrice, BaseEntity>>[]>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void GetAlbumsWithoutPriceTest()
        {
            AddArtistTest();

            using (var repository = _factory.GetAlbumRepository())
            {
                repository.AddOrUpdate(new Album { Name = "Some album" });
                repository.SaveChanges();
            }

            Assert.IsNotNull(_artistService.GetAlbumsWithoutPrice(1));

            Mock.Get(_factory.GetArtistRepository())
                .Verify(m => m.GetById(It.IsAny<int>()), Times.Once);

            Mock.Get(_factory.GetAlbumRepository())
                .Verify(m => m.GetAll(It.IsAny<Expression<Func<Album, bool>>>()), Times.Once);
        }

        [TestMethod]
        public void GetAlbumsWithPriceTest()
        {
            AddArtistTest();

            using (var repository = _factory.GetAlbumRepository())
            {
                repository.AddOrUpdate(new Album()
                {
                    Name = "Some album",
                    AlbumPrices = new List<AlbumPrice> { new AlbumPrice { Price = 4.99m } }
                });
                repository.SaveChanges();
            }

            Assert.IsNotNull(_artistService.GetAlbumsWithPrice(1));

            Mock.Get(_factory.GetArtistRepository())
                .Verify(m => m.GetById(It.IsAny<int>()), Times.Once);

            Mock.Get(_factory.GetAlbumRepository())
                .Verify(m => m.GetAll(It.IsAny<Expression<Func<Album, bool>>>()), Times.Once);

            Mock.Get(_factory.GetAlbumPriceRepository())
                .Verify(m => m.FirstOrDefault(It.IsAny<Expression<Func<AlbumPrice, bool>>>(),
                                              It.IsAny<Expression<Func<AlbumPrice, BaseEntity>>[]>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void GetArtistsListTest()
        {
            using (var repository = _factory.GetArtistRepository())
            {
                repository.AddOrUpdate(new Artist { Name = "Some artist" });
                repository.SaveChanges();
            }

            Assert.IsTrue(_artistService.GetArtistsList().Any());

            Mock.Get(_factory.GetArtistRepository())
                .Verify(m => m.GetAll(), Times.Once);

            Mock.Get(_factory.GetTrackRepository())
                .Verify(m => m.Count(It.IsAny<Expression<Func<Track, bool>>>()), Times.Once);

            Mock.Get(_factory.GetAlbumRepository())
                .Verify(m => m.Count(It.IsAny<Expression<Func<Album, bool>>>()), Times.Once);
        }
    }
}