using Shop.DAL.Infrastruture;

namespace PVT.Q1._2017.Shop.Tests
{
    using System.Linq;
    using global::Shop.BLL;
    using global::Shop.BLL.Exceptions;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Infrastructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;

    /// <summary>
    /// Summary description for TrackServiceTest
    /// </summary>
    [TestClass]
    public class TrackServiceTest
    {
        #region Fields

        private IKernel _kernel;

        #endregion //Fields

        #region Constructors

        public TrackServiceTest()
        {
            this._kernel = new StandardKernel(new DefaultServicesNinjectModule());
        }

        #endregion //Constructors

        #region Tests

        [TestMethod]
        public void RegisterValidTrackTest()
        {
            var repositoryFactory = this._kernel.Get<IFactory>();

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
            var trackService = this.GetTrackService();

            Assert.IsFalse(trackService.IsRegistered(track));

            trackService.Register(track);
            Assert.IsTrue(track.Id > 0);

            int trackId = track.Id;
            track = trackService.GetTrackInfo(trackId);
            Assert.IsTrue(track.Id > 0 && track.Artist.Id == artistId && track.Album.Id == albumId);

            Assert.IsTrue(trackService.IsRegistered(track));
        }

        [TestMethod]
        public void RegisterTrackWithFilledNavigationPropertiesTest()
        {
            var factory = this._kernel.Get<IFactory>();

            var artist = this.AddNewArtist(factory);
            Assert.IsTrue(artist != null && artist.Id > 0);

            var album = this.AddNewAlbum(factory, artist.Id);
            Assert.IsTrue(album != null && album.Id > 0);

            int artistId = artist.Id;
            int albumId = album.Id;
            var track = new Track
            {
                Name = "Super Track",
                AlbumId = albumId,
                Album = FindAlbum(factory, albumId),
                ArtistId = artistId,
                Artist = artist
            };
            var trackService = this.GetTrackService();

            Assert.IsFalse(trackService.IsRegistered(track));

            trackService.Register(track);
            Assert.IsTrue(track.Id > 0);

            int trackId = track.Id;
            track = trackService.GetTrackInfo(trackId);
            Assert.IsTrue(track.Id > 0 && track.Artist.Id == artistId && track.Album.Id == albumId);

            Assert.IsTrue(trackService.IsRegistered(track));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidEntityException))]
        public void RegisterInvalidTrackTest()
        {
            var trackService = this.GetTrackService();
            trackService.Register(new Track());
        }

        [TestMethod]
        public void TracksListTest()
        {
            RegisterValidTrackTest();

            var trackService = this.GetTrackService();
            Assert.IsTrue(trackService.GetTracksList().Any());
        }

        [TestMethod]
        public void TrackInfoTest()
        {
            RegisterValidTrackTest();

            var trackService = this.GetTrackService();

            var track = trackService.GetTracksList().FirstOrDefault(t => t.ArtistId.HasValue && t.AlbumId.HasValue);
            Assert.IsNotNull(track);

            track = trackService.GetTrackInfo(track.Id);
            Assert.IsNotNull(track);
            Assert.IsTrue(track.Artist != null && track.Album != null);
        }

        [TestMethod]
        public void TrackPricesTest()
        {
            this.RegisterValidTrackTest();
            var trackService = this.GetTrackService();
            Assert.IsTrue(trackService.GetTracksWithoutPriceConfigured().Any());
        }

        #endregion //Tests

        #region Private Methods

        private ITrackService GetTrackService()
        {
            return this._kernel.Get<ITrackService>();
        }

        private Artist AddNewArtist(IFactory factory)
        {
            var artist = new Artist { Name = "Super-puper Artist" };
            using (var repository = factory.Create<IArtistRepository>())
            {
                repository.AddOrUpdate(artist);
                repository.SaveChanges();
            }
            return artist;
        }

        private Album AddNewAlbum(IFactory factory, int artistId)
        {
            var album = new Album { Name = "Super-puper Album", ArtistId = artistId };
            using (var repository = factory.Create<IAlbumRepository>())
            {
                repository.AddOrUpdate(album);
                repository.SaveChanges();
            }
            return album;
        }

        private Album FindAlbum(IFactory factory, int id)
        {
            using (var repository = factory.Create<IAlbumRepository>())
            {
                return repository.GetById(id, a => a.Artist);
            }
        }

        #endregion //Private Methods
    }
}
