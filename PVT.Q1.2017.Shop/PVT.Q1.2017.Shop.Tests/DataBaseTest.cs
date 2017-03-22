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
    /// Summary description for DataBaseTest
    /// </summary>
    [TestClass]
    public class DataBaseTest
    {
        #region Fields

        private IKernel _kernel;

        #endregion //Fields

        #region Constructors

        public DataBaseTest()
        {
            this._kernel = new StandardKernel(new DefaultRepositoriesNinjectModule());
        }

        #endregion //Constructors

        #region Test

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

        [TestMethod]
        public void RepositoryFactoryTest()
        {
            var repositoryFactory = _kernel.Get<IRepositoryFactory>();
            Assert.IsNotNull(repositoryFactory);
        }

        [TestMethod]
        public void AddModelTest()
        {
            string trackName = "Hello";

            var repositoryFactory = _kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.GetTrackRepository())
            {
                repository.AddOrUpdate(new Track { Name = trackName });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(t => t.Name == trackName).Any());
            }
        }

        [TestMethod]
        public void GetAllModelWithExpressionTest()
        {
            string artist1Name = "Artist 1";
            string artist2Name = "Artist 2";
            string artist3Name = "Artist 3";

            var repositoryFactory = _kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.GetArtistRepository())
            {
                repository.AddOrUpdate(new Artist { Name = artist1Name });
                repository.AddOrUpdate(new Artist { Name = artist2Name });
                repository.AddOrUpdate(new Artist { Name = artist3Name });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(a => a.Name.StartsWith("Artist")).Count >= 3);
            }
        }

        [TestMethod]
        public void GetAllModelWithoutExpressionTest()
        {
            string album1Name = "Album 1";
            string album2Name = "Album 2";
            string album3Name = "Album 3";

            var repositoryFactory = _kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.GetAlbumRepository())
            {
                repository.AddOrUpdate(new Album { Name = album1Name });
                repository.AddOrUpdate(new Album { Name = album2Name });
                repository.AddOrUpdate(new Album { Name = album3Name });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Count >= 3, repository.GetAll().Count.ToString());
            }
        }

        [TestMethod]
        public void UpdateModelTest()
        {
            string trackName = "Super-puper track with duration";

            var repositoryFactory = _kernel.Get<IRepositoryFactory>();
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

        [TestMethod]
        public void ArtistTracksTest()
        {
            var artist = new Artist { Name = "Sia" };

            var repositoryFactory = _kernel.Get<IRepositoryFactory>();
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

        [TestMethod]
        public void TracksWithArtistsTest()
        {
            var repositoryFactory = _kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.GetTrackRepository())
            {
                Assert.IsTrue(repository.GetAll(t => t.ArtistId != null).Any());
            }
        }

        [TestMethod]
        public void RegisterValidTrackTest()
        {
            var repositoryFactory = this._kernel.Get<IRepositoryFactory>();

            var artist = this.AddNewArtist(repositoryFactory);
            Assert.IsTrue(artist != null && artist.Id > 0);

            var album = this.AddNewAlbum(repositoryFactory, artist.Id);
            Assert.IsTrue(album != null && album.Id > 0);

            int artistId = artist.Id;
            int albumId = album.Id;
            var track = new Track
            {
                Name = "Super Track",
                AlbumId = albumId,
                ArtistId = artistId
            };

            using (var repository = repositoryFactory.GetTrackRepository())
            {
                repository.AddOrUpdate(track);
                repository.SaveChanges();
            }

            Assert.IsTrue(track.Id > 0);

            int trackId = track.Id;
            using (var repository = repositoryFactory.GetTrackRepository())
            {
                track = repository.GetById(trackId);
                Assert.IsNotNull(track);
                Assert.IsTrue(track.Id > 0 && track.Artist.Id == artistId);
            }
        }

        [TestMethod]
        public void RegisterTrackWithFilledNavigationPropertiesTest()
        {
            var repositoryFactory = this._kernel.Get<IRepositoryFactory>();

            var artist = this.AddNewArtist(repositoryFactory);
            Assert.IsTrue(artist != null && artist.Id > 0);

            var album = this.AddNewAlbum(repositoryFactory, artist.Id);
            Assert.IsTrue(album != null && album.Id > 0);

            int artistId = artist.Id;
            int albumId = album.Id;
            var track = new Track
            {
                Name = "Super Track",
                AlbumId = albumId,
                ArtistId = artistId,
                Artist = artist
            };

            using (var repository = repositoryFactory.GetTrackRepository())
            {
                repository.AddOrUpdate(track);
                repository.SaveChanges();
            }

            Assert.IsTrue(track.Id > 0);

            int trackId = track.Id;
            using (var repository = repositoryFactory.GetTrackRepository())
            {
                track = repository.GetById(trackId);
                Assert.IsNotNull(track);
                Assert.IsTrue(track.Id > 0 && track.Artist.Id == artistId);
            }
        }

        #endregion //Tests

        #region Private Methods

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
