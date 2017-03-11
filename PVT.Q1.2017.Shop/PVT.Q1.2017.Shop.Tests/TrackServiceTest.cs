namespace PVT.Q1._2017.Shop.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using global::Shop.BLL.Services;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

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
            this._factory = new RepositoryMoqFactory();
            this._trackService = new TrackService(this._factory);
        }

        [TestMethod]
        public void AddTrackTest()
        {
            using (var repository = this._factory.GetTrackRepository())
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
            Assert.IsTrue(this._trackService.GetTracksList().Any());
        }

        [TestMethod]
        public void GetTrackInfoTest()
        {
            AddTrackTest();
            Assert.IsNotNull(this._trackService.GetTrackInfo(1));
        }

        [TestMethod]
        public void GetTracksWithPriceConfiguredTest()
        {
            AddTrackTest();

            var track = this._trackService.GetTracksList().FirstOrDefault();
            Assert.IsNotNull(track);

            track.TrackPrices = new List<TrackPrice> { new TrackPrice { Price = 1.99m } };

            Assert.IsTrue(_trackService.GetTracksWithPriceConfigured().Any());
        }

        [TestMethod]
        public void GetTracksWithoutPriceConfiguredTest()
        {
            AddTrackTest();
            Assert.IsTrue(_trackService.GetTracksWithoutPriceConfigured().Any());
        }

        [TestMethod]
        public void GetTrackPricesTest()
        {
            AddTrackTest();

            var track = this._trackService.GetTracksList().FirstOrDefault();
            Assert.IsNotNull(track);

            using (var repository = this._factory.GetTrackPriceRepository())
            {
                repository.AddOrUpdate(new TrackPrice { Track = track, TrackId = track.Id, Price = 4.99m });
                repository.SaveChanges();
            }
            Assert.IsTrue(_trackService.GetTrackPrices(new Track()).Any());
        }

        [TestMethod]
        public void GetTrackVotesTest()
        {
            AddTrackTest();

            var track = this._trackService.GetTracksList().FirstOrDefault();
            Assert.IsNotNull(track);

            using (var repository = this._factory.GetVoteRepository())
            {
                repository.AddOrUpdate(new Vote { Track = track, TrackId = track.Id, Mark = Mark.FiveStars });
                repository.SaveChanges();
            }
            Assert.IsTrue(_trackService.GetTrackVotes(new Track()).Any());
        }

        [TestMethod]
        public void GetTrackFeedbacksTest()
        {
            AddTrackTest();

            var track = this._trackService.GetTracksList().FirstOrDefault();
            Assert.IsNotNull(track);

            using (var repository = this._factory.GetFeedbackRepository())
            {
                repository.AddOrUpdate(new Feedback { Track = track, TrackId = track.Id, Comments = "Some comments" });
                repository.SaveChanges();
            }
            Assert.IsTrue(_trackService.GetTrackFeedbacks(new Track()).Any());
        }

        [TestMethod]
        public void GetAlbumsList()
        {
            AddTrackTest();

            var track = this._trackService.GetTracksList().FirstOrDefault();
            Assert.IsNotNull(track);

            using (var repository = this._factory.GetAlbumRepository())
            {
                repository.AddOrUpdate(new Album
                {
                    ArtistId = track.ArtistId,
                    Name = "Some album"
                });
                repository.SaveChanges();
            }

            using (var repository = this._factory.GetAlbumTrackRelationRepository())
            {
                repository.AddOrUpdate(new AlbumTrackRelation
                {
                    TrackId = 1,
                    AlbumId = 2
                });
            }

                Assert.IsTrue(_trackService.GetAlbumsList(new Track()).Any());
        }
    }
}
