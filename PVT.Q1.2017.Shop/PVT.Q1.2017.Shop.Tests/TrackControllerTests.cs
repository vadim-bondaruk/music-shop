namespace PVT.Q1._2017.Shop.Tests
{
    using System.Collections.ObjectModel;
    using System.Web.Mvc;

    using global::Moq;

    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PVT.Q1._2017.Shop.Areas.Management.Controllers;
    using PVT.Q1._2017.Shop.Tests.Moq;

    /// <summary>
    /// </summary>
    [TestClass]
    public class TrackControllerTests
    {
        /// <summary>
        /// </summary>
        private TrackController controller;

        /// <summary>
        /// </summary>
        private ActionResult result;

        /// <summary>
        /// </summary>
        [TestInitialize]
        public void SetupContext()
        {
            this.controller = new TrackController();
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestAddNew()
        {
            var aresult = this.controller.NewTrack();
            Assert.IsNotNull(aresult);
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestAlbumsListWithoutParameters()
        {
            var mockFactory = new Mock<IRepositoryFactory>();
            mockFactory.Setup(m => m.GetAlbumRepository()).Returns(new Mock<IAlbumRepository>().Object);
            var trackController = new TrackController(mockFactory.Object);
            Assert.IsNotNull(trackController.AlbumsList());
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestGetAlbumsListByArtistId()
        {
            var mockFactory = new Mock<IRepositoryFactory>();
            var mockRepo = new Mock<IAlbumRepository>();
            mockRepo.Setup(m => m.GetAll()).Returns(new Collection<Album> { new Album { Id = 1, Name = "SomeAlbum" } });
            mockFactory.Setup(m => m.GetAlbumRepository()).Returns(mockRepo.Object);
            var trackController = new TrackController(mockFactory.Object);
            Assert.IsNotNull(trackController.AlbumsList(1));
        }

        /// <summary>
        /// </summary>
        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestGetAlbumsListByTrackId()
        {
            var mockFactory = new Mock<IRepositoryFactory>();
            var mockRepo = new Mock<IAlbumRepository>();
            mockRepo.Setup(m => m.GetAll(a => a.TrackId.Equals(1)))
                .Returns(
                    new Collection<Album>
                        {
                            new Album { Id = 1, Name = "SomeAlbum1" },
                            new Album { Id = 2, Name = "SomeAlbum2" }
                        });
            mockFactory.Setup(m => m.GetAlbumRepository()).Returns(mockRepo.Object);
            var trackController = new TrackController(mockFactory.Object);
            Assert.IsNotNull(trackController.AlbumsListByTrackId(1));
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestGetAllALbumsListCount()
        {
            var mockAlbumRepo = new MoqGenerator().GetAlbumRepoMoq();
            Assert.AreEqual(mockAlbumRepo.GetAll().Count, 2);
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestGetAllArtists()
        {
            var mockFactory = new Mock<IRepositoryFactory>();
            var mockRepo = new Mock<IArtistRepository>();
            mockRepo.Setup(m => m.GetAll())
                .Returns(
                    new Collection<Artist>
                        {
                            new Artist { Id = 1, Name = "SomeArtist1" },
                            new Artist { Id = 2, Name = "SomeArtist2" }
                        });
            mockFactory.Setup(m => m.GetArtistRepository()).Returns(mockRepo.Object);
            var trackController = new TrackController(mockFactory.Object);
            Assert.IsNotNull(trackController.ArtistList(1));
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestGetArtistById()
        {
            var mockFactory = new Mock<IRepositoryFactory>();
            var mockRepo = new Mock<IArtistRepository>();
            mockRepo.Setup(m => m.GetAll(a => a.Id.Equals(1)))
                .Returns(new Collection<Artist> { new Artist { Id = 1, Name = "SomeArtist" } });
            mockFactory.Setup(m => m.GetArtistRepository()).Returns(mockRepo.Object);
            var trackController = new TrackController(mockFactory.Object);
            Assert.IsNotNull(trackController.ArtistList(1));
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestGetArtistTracksByArtistId()
        {
            var mockFactory = new Mock<IRepositoryFactory>();
            var mockRepo = new Mock<ITrackRepository>();
            mockRepo.Setup(m => m.GetAll(a => a.Artist.Id.Equals(1)))
                .Returns(new Collection<Track> { new Track { Id = 1, Name = "SomeArtist" } });
            mockFactory.Setup(m => m.GetTrackRepository()).Returns(mockRepo.Object);
            var trackController = new TrackController(mockFactory.Object);
            Assert.IsNotNull(trackController.TrackListByAtistId(1));
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestGetTrackDetails()
        {
            var mockFactory = new Mock<IRepositoryFactory>();
            var mockRepo = new Mock<ITrackRepository>();
            mockRepo.Setup(m => m.GetAll(a => a.Id.Equals(1)))
                .Returns(new Collection<Track> { new Track { Id = 1, Name = "SomeArtist" } });
            mockFactory.Setup(m => m.GetTrackRepository()).Returns(mockRepo.Object);
            var trackController = new TrackController(mockFactory.Object);
            Assert.IsNotNull(trackController.TrackList(1));
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestGetTrackistByAlbumId()
        {
            var mockFactory = new Mock<IRepositoryFactory>();
            var mockRepo = new Mock<ITrackRepository>();
            mockRepo.Setup(m => m.GetAll(a => a.AlbumId.Equals(1)))
                .Returns(new Collection<Track> { new Track { Id = 1, Name = "SomeArtist" } });
            mockFactory.Setup(m => m.GetTrackRepository()).Returns(mockRepo.Object);
            var trackController = new TrackController(mockFactory.Object);
            Assert.IsNotNull(trackController.TrackListByAlbumId(1));
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestGetTrackListByArtistId()
        {
            var mockFactory = new Mock<IRepositoryFactory>();
            var mockRepo = new Mock<ITrackRepository>();
            mockRepo.Setup(m => m.GetAll(a => a.AlbumId.Equals(1)))
                .Returns(new Collection<Track> { new Track { Id = 1, Name = "SomeArtist" } });
            mockFactory.Setup(m => m.GetTrackRepository()).Returns(mockRepo.Object);
            var trackController = new TrackController(mockFactory.Object);
            Assert.IsNotNull(trackController.TrackListByAtistId(1));
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestViewAllTracks()
        {
            var mockFactory = new Mock<IRepositoryFactory>();
            var mockRepo = new Mock<ITrackRepository>();
            mockRepo.Setup(m => m.GetAll(a => a.AlbumId.Equals(1)))
                .Returns(new Collection<Track> { new Track { Id = 1, Name = "SomeArtist" } });
            mockFactory.Setup(m => m.GetTrackRepository()).Returns(mockRepo.Object);
            var trackController = new TrackController(mockFactory.Object);
            Assert.IsNotNull(trackController.TrackList());
        }
    }
}