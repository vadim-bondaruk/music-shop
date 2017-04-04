﻿namespace PVT.Q1._2017.Shop.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using global::Shop.BLL;
    using global::Shop.BLL.Services;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using global::Moq;

    [TestClass]
    public class AlbumServiceTest
    {
        private readonly IAlbumService _albumService;
        private readonly IRepositoryFactory _factory;

        public AlbumServiceTest()
        {
            _factory = new RepositoryFactoryMoq();
            _albumService = new AlbumService(_factory);
        }

        [TestMethod]
        public void AddAlbumTest()
        {
            using (var repository = _factory.GetAlbumRepository())
            {
                repository.AddOrUpdate(new Album { Name = "Some album" });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Any());
            }
        }

        [TestMethod]
        public void GetAlbumsListTest()
        {
            AddAlbumTest();
            Assert.IsTrue(_albumService.GetAlbumsList().Any());

            Mock.Get(_factory.GetAlbumRepository()).Verify(m => m.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetTracksListTest()
        {
            AddAlbumTest();

            using (var repository = _factory.GetTrackRepository())
            {
                repository.AddOrUpdate(new Track { Name = "Some track" });
                repository.SaveChanges();
            }

            using (var repository = _factory.GetAlbumTrackRelationRepository())
            {
                repository.AddOrUpdate(new AlbumTrackRelation
                {
                    TrackId = 1,
                    AlbumId = 2
                });
            }

            Assert.IsNotNull(_albumService.GetTracksList(1));

            Mock.Get(_factory.GetAlbumTrackRelationRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<AlbumTrackRelation, bool>>>(),
                                     It.IsAny<Expression<Func<AlbumTrackRelation, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetAlbumInfoTest()
        {
            AddAlbumTest();
            Assert.IsNotNull(_albumService.GetAlbumDetails(1));

            Mock.Get(_factory.GetAlbumRepository())
                .Verify(m => m.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<Album, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetAlbumsWithPriceConfiguredTest()
        {
            AddAlbumTest();

            var album = _albumService.GetAlbumsList().FirstOrDefault();
            Assert.IsNotNull(album);

            Assert.IsTrue(_albumService.GetAlbumsWithPrice().Any());

            Mock.Get(_factory.GetAlbumRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Album, bool>>>(),
                                     It.IsAny<Expression<Func<Album, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetAlbumsWithoutPriceConfiguredTest()
        {
            AddAlbumTest();
            Assert.IsTrue(_albumService.GetAlbumsWithoutPrice().Any());

            Mock.Get(_factory.GetAlbumRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Album, bool>>>(),
                                     It.IsAny<Expression<Func<Album, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetTracksWithPriceConfiguredTest()
        {
            AddAlbumTest();

            using (var repository = _factory.GetTrackRepository())
            {
                repository.AddOrUpdate(new Track { Name = "Some track" });
                repository.SaveChanges();
            }

            using (var repository = _factory.GetAlbumTrackRelationRepository())
            {
                repository.AddOrUpdate(new AlbumTrackRelation
                {
                    TrackId = 1,
                    Track = new Track
                    {
                        Id = 1,
                        Name = "Some track",
                        TrackPrices = new List<TrackPrice> { new TrackPrice { Price = 1.99m } }
                    },
                    AlbumId = 2,
                    Album = new Album { Id = 2, Name = "Some album" }
                });
                repository.SaveChanges();
            }

            Assert.IsNotNull(_albumService.GetTracksWithPrice(1));

            Mock.Get(_factory.GetAlbumTrackRelationRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<AlbumTrackRelation, bool>>>(),
                                     It.IsAny<Expression<Func<AlbumTrackRelation, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetTracksWithoutPriceConfiguredTest()
        {
            AddAlbumTest();

            using (var repository = _factory.GetTrackRepository())
            {
                repository.AddOrUpdate(new Track { Name = "Some track" });
                repository.SaveChanges();
            }

            using (var repository = _factory.GetAlbumTrackRelationRepository())
            {
                repository.AddOrUpdate(new AlbumTrackRelation
                {
                    TrackId = 1,
                    AlbumId = 2
                });
            }

            Assert.IsNotNull(_albumService.GetTracksWithoutPrice(1));

            Mock.Get(_factory.GetAlbumTrackRelationRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<AlbumTrackRelation, bool>>>(),
                                     It.IsAny<Expression<Func<AlbumTrackRelation, BaseEntity>>[]>()), Times.Once);
        }
    }
}