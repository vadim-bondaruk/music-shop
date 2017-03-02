namespace PVT.Q1._2017.Shop.Tests
{
    using global::Shop.BLL;
    using global::Shop.BLL.Exceptions;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Repositories.Infrastruture;
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

            int artistId = this.AddNewArtist(repositoryFactory);
            Assert.IsTrue(artistId > 0);

            int albumId = this.AddNewAlbum(repositoryFactory, artistId);
            Assert.IsTrue(albumId > 0);

            var track = new Track
            {
                Name = "Super Track",
                AlbumId = albumId,
                ArtistId = artistId
            };
            var trackService = this._kernel.Get<ITrackService>();
            trackService.Register(track);
            Assert.IsTrue(track.Id > 0);

            int trackId = track.Id;
            using (var repository = repositoryFactory.Create<ITrackRepository>())
            {
                track = repository.GetById(trackId, t => t.Artist, t => t.Album);
                Assert.IsTrue(track.Id > 0 && track.Artist.Id == artistId && track.Album.Id == albumId);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidEntityException))]
        public void RegisterInvalidTrackTest()
        {
            var trackService = this._kernel.Get<ITrackService>();
            trackService.Register(new Track());
        }

        #endregion //Tests

        #region Private Methods

        private int AddNewArtist(IFactory repositoryFactory)
        {
            var artist = new Artist { Name = "Super-puper Artist" };
            using (var repository = repositoryFactory.Create<IArtistRepository>())
            {
                repository.AddOrUpdate(artist);
                repository.SaveChanges();
            }
            return artist.Id;
        }

        private int AddNewAlbum(IFactory repositoryFactory, int artistId)
        {
            var album = new Album { Name = "Super-puper Album", ArtistId = artistId };
            using (var repository = repositoryFactory.Create<IAlbumRepository>())
            {
                repository.AddOrUpdate(album);
                repository.SaveChanges();
            }
            return album.Id;
        }

        #endregion //Private Methods
    }
}
