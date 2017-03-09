//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Track.cs" company="PVT.Q1.2017">
//    PVT.Q1.2017
//  </copyright>
//  <summary>
//    The track.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace PVT.Q1._2017.Shop.Tests
{
    using System;
    using System.Linq;

    using global::Shop.Common.Models;
    using global::Shop.DAL;
    using global::Shop.DAL.Context;
    using global::Shop.DAL.Infrastruture;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ninject;

    /// <summary>
    ///     Summary description for DataBaseTest
    /// </summary>
    [TestClass]
    public class DataBaseTest
    {
        #region Fields

        /// <summary>
        /// </summary>
        private readonly IKernel _kernel;

        #endregion //Fields

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataBaseTest" /> class.
        /// </summary>
        public DataBaseTest()
        {
            this._kernel = new StandardKernel(new DefaultRepositoriesNinjectModule());
        }

        #endregion //Constructors

        #region Test

        /// <summary>
        /// </summary>
        [TestMethod]
        public void CreateDataBaseTest()
        {
            using (var context = new ShopContext())
            {
                if (context.Database.Exists())
                {
                    context.Database.Delete();
                }

                context.Database.Create();
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void RepositoryFactoryTest()
        {
            var repositoryFactory = this._kernel.Get<IRepositoryFactory>();
            Assert.IsNotNull(repositoryFactory);
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void AddModelTest()
        {
            var trackName = "Hello";

            var repositoryFactory = this._kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.GetTrackRepository())
            {
                repository.AddOrUpdate(new Track { Name = trackName });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(t => t.Name == trackName).Any());
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetAllModelWithExpressionTest()
        {
            var artist1Name = "Artist 1";
            var artist2Name = "Artist 2";
            var artist3Name = "Artist 3";

            var repositoryFactory = this._kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.GetArtistRepository())
            {
                repository.AddOrUpdate(new Artist { Name = artist1Name });
                repository.AddOrUpdate(new Artist { Name = artist2Name });
                repository.AddOrUpdate(new Artist { Name = artist3Name });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(a => a.Name.StartsWith("Artist")).Count >= 3);
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetAllModelWithoutExpressionTest()
        {
            var album1Name = "Album 1";
            var album2Name = "Album 2";
            var album3Name = "Album 3";

            var repositoryFactory = this._kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.GetAlbumRepository())
            {
                repository.AddOrUpdate(new Album { Name = album1Name });
                repository.AddOrUpdate(new Album { Name = album2Name });
                repository.AddOrUpdate(new Album { Name = album3Name });
                repository.SaveChanges();

                // Assert.IsTrue(repository.GetAll().Count >= 3, repository.GetAll().Count.ToString());
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void UpdateModelTest()
        {
            var trackName = "Super-puper track with duration";

            var repositoryFactory = this._kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.GetTrackRepository())
            {
                repository.AddOrUpdate(new Track { Name = trackName });
                repository.SaveChanges();
            }

            var duration = new TimeSpan(0, 2, 46);

            using (var repository = repositoryFactory.GetTrackRepository())
            {
                var track = repository.GetAll(t => t.Name == trackName).FirstOrDefault();
                Assert.IsNotNull(track);

                track.Duration = duration;

                repository.AddOrUpdate(track);
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(t => t.Duration == duration).Any());
            }

            using (var repository = repositoryFactory.GetTrackRepository())
            {
                Assert.IsTrue(repository.GetAll(t => t.Duration == duration).Any());
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void ArtistTracksTest()
        {
            var artist = new Artist { Name = "Sia" };

            var repositoryFactory = this._kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.GetArtistRepository())
            {
                repository.AddOrUpdate(artist);
                repository.SaveChanges();
            }

            var track1 = new Track { Name = "Unstoppable", ArtistId = artist.Id };
            var track2 = new Track { Name = "Alive", ArtistId = artist.Id };
            using (var repository = repositoryFactory.GetTrackRepository())
            {
                repository.AddOrUpdate(track1);
                repository.AddOrUpdate(track2);
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(t => t.ArtistId != null).Any());
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TracksWithArtistsTest()
        {
            var repositoryFactory = this._kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.GetTrackRepository())
            {
                Assert.IsTrue(repository.GetAll(t => t.ArtistId != null).Any());
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void RegisterValidTrackTest()
        {
            var repositoryFactory = this._kernel.Get<IRepositoryFactory>();

            var artist = this.AddNewArtist(repositoryFactory);
            Assert.IsTrue(artist != null && artist.Id > 0);

            var album = this.AddNewAlbum(repositoryFactory, artist.Id);
            Assert.IsTrue(album != null && album.Id > 0);

            var artistId = artist.Id;
            var albumId = album.Id;
            var track = new Track { Name = "Super Track", AlbumId = albumId, ArtistId = artistId };

            using (var repository = repositoryFactory.GetTrackRepository())
            {
                repository.AddOrUpdate(track);
                repository.SaveChanges();
            }

            Assert.IsTrue(track.Id > 0);

            var trackId = track.Id;
            using (var repository = repositoryFactory.GetTrackRepository())
            {
                track = repository.GetById(trackId);
                Assert.IsNotNull(track);
                Assert.IsTrue(track.Id > 0 && track.Artist.Id == artistId);
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void RegisterTrackWithFilledNavigationPropertiesTest()
        {
            var repositoryFactory = this._kernel.Get<IRepositoryFactory>();

            var artist = this.AddNewArtist(repositoryFactory);
            Assert.IsTrue(artist != null && artist.Id > 0);

            var album = this.AddNewAlbum(repositoryFactory, artist.Id);
            Assert.IsTrue(album != null && album.Id > 0);

            var artistId = artist.Id;
            var albumId = album.Id;
            var track = new Track { Name = "Super Track", AlbumId = albumId, ArtistId = artistId, Artist = artist };

            using (var repository = repositoryFactory.GetTrackRepository())
            {
                repository.AddOrUpdate(track);
                repository.SaveChanges();
            }

            Assert.IsTrue(track.Id > 0);

            var trackId = track.Id;
            using (var repository = repositoryFactory.GetTrackRepository())
            {
                track = repository.GetById(trackId);
                Assert.IsNotNull(track);
                Assert.IsTrue(track.Id > 0 && track.Artist.Id == artistId);
            }
        }

        #endregion //Tests

        #region Private Methods

        /// <summary>
        /// </summary>
        /// <param name="factory">
        ///     The factory.
        /// </param>
        /// <returns>
        /// </returns>
        private Artist AddNewArtist(IRepositoryFactory factory)
        {
            var artist = new Artist { Name = "Super-puper Artist" };
            using (var repository = factory.GetArtistRepository())
            {
                repository.AddOrUpdate(artist);
                repository.SaveChanges();
            }

            return artist;
        }

        /// <summary>
        /// </summary>
        /// <param name="factory">
        ///     The factory.
        /// </param>
        /// <param name="artistId">
        ///     The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        private Album AddNewAlbum(IRepositoryFactory factory, int artistId)
        {
            var album = new Album { Name = "Super-puper Album", ArtistId = artistId };
            using (var repository = factory.GetAlbumRepository())
            {
                repository.AddOrUpdate(album);
                repository.SaveChanges();
            }

            return album;
        }

        #endregion //Private Methods
    }
}