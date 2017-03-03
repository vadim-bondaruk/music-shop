namespace PVT.Q1._2017.Shop.Tests
{
    using System.Linq;
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
            var trackService = this.GetTrackService();
            Assert.IsTrue(trackService.GetTracksList().Any());
        }

        [TestMethod]
        public void TrackInfoTest()
        {
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
            var trackService = this.GetTrackService();

            var track = trackService.GetTracksList().FirstOrDefault(t => t.ArtistId.HasValue && t.AlbumId.HasValue);
            Assert.IsNotNull(track);

            

            track = trackService.GetTrackInfo(track.Id);
            Assert.IsNotNull(track);
            Assert.IsTrue(track.Artist != null && track.Album != null);
        }

        #endregion //Tests

        #region Private Methods

        private ITrackService GetTrackService()
        {
            return this._kernel.Get<ITrackService>();
        }

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
