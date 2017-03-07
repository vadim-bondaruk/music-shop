namespace PVT.Q1._2017.Shop.Tests
{
    #region using

    using global::Shop.DAL;
    using global::Shop.DAL.Repositories.Infrastruture;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ninject;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TrackControllerTests
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly StandardKernel _kernel;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="TrackControllerTests" /> class.
        /// </summary>
        public TrackControllerTests()
        {
            this._kernel = new StandardKernel(new DefaultRepositoriesNinjectModule());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestAlbumListByArtistId()
        {
            using (var albumRepository = this._kernel.Get<IAlbumRepository>())
            {
                var albumsList = albumRepository.GetAll(a => a.Id == 1);
                Assert.IsNotNull(albumsList);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestAlbumsListByTrack()
        {
            var trackRepository = this._kernel.Get<ITrackRepository>();
            var tracksItem = trackRepository.GetById(1);

            using (var albumRepository = this._kernel.Get<IAlbumRepository>())
            {
                var albumsList = albumRepository.GetAll(a => a.Tracks.Contains(tracksItem));
                Assert.IsNotNull(albumsList);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestArtistList()
        {
            using (var trackRepository = this._kernel.Get<ITrackRepository>())
            {
                trackRepository.GetAll();
                Assert.IsNotNull(trackRepository);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestArtistTracks()
        {
            using (var trackRepository = this._kernel.Get<ITrackRepository>())
            {
                var tracks = trackRepository.GetAll(t => t.Artist.Id == 1);
                Assert.IsNotNull(tracks);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestTrackDetails()
        {
            using (var trackRepository = this._kernel.Get<ITrackRepository>())
            {
                var trackDetails = trackRepository.GetAll(t => t.Id == 1);
                Assert.IsNotNull(trackDetails);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestViewAllTracks()
        {
            using (var trackRepository = this._kernel.Get<ITrackRepository>())
            {
                var trackDetails = trackRepository.GetAll();
                Assert.IsNotNull(trackDetails);
            }
        }
    }
}