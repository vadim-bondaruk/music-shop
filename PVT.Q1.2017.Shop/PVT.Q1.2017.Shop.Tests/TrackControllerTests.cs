namespace PVT.Q1._2017.Shop.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using global::Moq;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ninject;

    using PVT.Q1._2017.Shop.Tests.Moq;

    /// <summary>
    /// </summary>
    [TestClass]
    public class TrackControllerTests
    {
        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory _factory;

        /// <summary>
        /// </summary>
        private readonly StandardKernel _kernel;

        /// <summary>
        /// </summary>
        private readonly ITrackService _trackService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TrackControllerTests" /> class.
        /// </summary>
        public TrackControllerTests()
        {
            IKernel kernel = new StandardKernel(new DefaultRepositoriesNinjectModule());
            this._factory = kernel.Get<IRepositoryFactory>();
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestGetAlbumListByArtistId()
        {
            var mockAlbumRepo = new MoqGenerator().GetAlbumRepoMoq();
            Assert.IsNotNull(mockAlbumRepo.GetById(1));
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestGetAlbumsListByTrack()
        {
            var mock = new MoqGenerator().GetAlbumRepoMoq();
            var albumsList =
                mock.GetAll(
                    a =>
                        a.Tracks.Contains(
                            new Track
                                {
                                    Id = 22,
                                    Name = "SomeTrack",
                                    Artist =
                                        new Artist
                                            {
                                                Id = 22,
                                                Name = "SomeArtist",
                                                Albums =
                                                    new List<Album>
                                                        {
                                                            new Album
                                                                {
                                                                    Id = 22,
                                                                    Name = "SomeAlbum",
                                                                    ReleaseDate =
                                                                        DateTime.Now
                                                                }
                                                        }
                                            }
                                }));
            Assert.IsNotNull(albumsList);
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestGetArtistTracks()
        {
            var tracksRepo = new MoqGenerator().GetTrackRepoMoq();
            ValueType tracksCount = tracksRepo.GetAll(t => t.Artist.Id == 22)?.Count;
            Assert.AreEqual(tracksCount, 2);
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
        public void TestGetTrackDetails()
        {
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestGetTrackListByArtistId()
        {
            var trackRepoMoq = new MoqGenerator().GetTrackRepoMoq();
            var trackList = trackRepoMoq.GetAll(t => t.Artist.Id == 22);

            // Assert.AreEqual(trackList.Count, 1);
            // var track = trackRepoMoq.GetById(22, t => t.ArtistId);
            // Assert.AreEqual(track.Name, "SomeTrack");
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestViewAllTracks()
        {
        }
    }
}