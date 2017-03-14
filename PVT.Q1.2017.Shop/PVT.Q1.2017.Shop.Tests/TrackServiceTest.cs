namespace PVT.Q1._2017.Shop.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using global::Moq;
    using global::Shop.BLL;
    using global::Shop.BLL.Services;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    /// <summary>
    /// Summary description for TrackServiceTest
    /// </summary>
    [TestClass]
    public class TrackServiceTest
    {
        private readonly ITrackService _trackService;
        private readonly IRepositoryFactory _factory;

        public TrackServiceTest()
        {
            _factory = new RepositoryFactoryMoq();
            _trackService = new TrackService(_factory);
            DefaultModelsMapper.MapModels();
        }

        [TestMethod]
        public void AddTrackTest()
        {
            using (var repository = _factory.GetTrackRepository())
            {
                repository.AddOrUpdate(new Track { Name = "Some track" });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Any());
            }
        }

        [TestMethod]
        public void GetTracksListTest()
        {
            AddTrackTest();
            Assert.IsTrue(_trackService.GetTracksList().Any());

            Mock.Get(_factory.GetTrackRepository()).Verify(m => m.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetTrackInfoTest()
        {
            AddTrackTest();
            Assert.IsNotNull(_trackService.GetTrackInfo(1));

            Mock.Get(_factory.GetTrackRepository())
                .Verify(m => m.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<Track, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetTracksWithPriceConfiguredTest()
        {
            AddTrackTest();

            var track = _trackService.GetTracksList().FirstOrDefault();
            Assert.IsNotNull(track);

            track.TrackPrices = new List<TrackPrice> { new TrackPrice { Price = 1.99m } };

            Assert.IsTrue(_trackService.GetTracksWithPriceConfigured().Any());

            Mock.Get(_factory.GetTrackRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Track, bool>>>(),
                                     It.IsAny<Expression<Func<Track, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetTracksWithoutPriceConfiguredTest()
        {
            AddTrackTest();
            Assert.IsTrue(_trackService.GetTracksWithoutPriceConfigured().Any());

            Mock.Get(_factory.GetTrackRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Track, bool>>>(),
                                     It.IsAny<Expression<Func<Track, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetTrackPricesTest()
        {
            AddTrackTest();

            var track = _trackService.GetTracksList().FirstOrDefault();
            Assert.IsNotNull(track);

            using (var repository = _factory.GetTrackPriceRepository())
            {
                repository.AddOrUpdate(new TrackPrice { Track = track, TrackId = track.Id, Price = 4.99m });
                repository.SaveChanges();
            }

            Assert.IsTrue(_trackService.GetTrackPrices(new Track()).Any());

            Mock.Get(_factory.GetTrackPriceRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<TrackPrice, bool>>>(),
                                     It.IsAny<Expression<Func<TrackPrice, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetTrackVotesTest()
        {
            AddTrackTest();

            var track = _trackService.GetTracksList().FirstOrDefault();
            Assert.IsNotNull(track);

            using (var repository = _factory.GetVoteRepository())
            {
                repository.AddOrUpdate(new Vote { Track = track, TrackId = track.Id, Mark = Mark.FiveStars });
                repository.SaveChanges();
            }

            Assert.IsTrue(_trackService.GetTrackVotes(new Track()).Any());

            Mock.Get(_factory.GetVoteRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Vote, bool>>>(),
                                     It.IsAny<Expression<Func<Vote, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetTrackFeedbacksTest()
        {
            AddTrackTest();

            var track = _trackService.GetTracksList().FirstOrDefault();
            Assert.IsNotNull(track);

            using (var repository = _factory.GetFeedbackRepository())
            {
                repository.AddOrUpdate(new Feedback { Track = track, TrackId = track.Id, Comments = "Some comments" });
                repository.SaveChanges();
            }

            Assert.IsTrue(_trackService.GetTrackFeedbacks(new Track()).Any());

            Mock.Get(_factory.GetFeedbackRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Feedback, bool>>>(),
                                     It.IsAny<Expression<Func<Feedback, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetAlbumsList()
        {
            AddTrackTest();

            var track = _trackService.GetTracksList().FirstOrDefault();
            Assert.IsNotNull(track);

            using (var repository = _factory.GetAlbumRepository())
            {
                repository.AddOrUpdate(new Album
                {
                    ArtistId = track.ArtistId,
                    Name = "Some album"
                });
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

            Assert.IsTrue(_trackService.GetAlbumsList(new Track(), new Currency(), new PriceLevel()).Albums.Any());

            Mock.Get(_factory.GetAlbumRepository())
                .Verify(m => m.GetAll(It.IsAny<Expression<Func<Album, bool>>>()), Times.Once);
        }
    }
}
