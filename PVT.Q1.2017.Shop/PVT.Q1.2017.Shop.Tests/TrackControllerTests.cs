namespace PVT.Q1._2017.Shop.Tests
{
    using global::Shop.DAL;
    using global::Shop.Infrastructure.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ninject;

    using PVT.Q1._2017.Shop.Controllers;

    /// <summary>
    /// </summary>
    [TestClass]
    public class TrackControllerTests
    {
        /// <summary>
        /// </summary>
        private StandardKernel _kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackControllerTests"/> class.
        /// </summary>
        public TrackControllerTests()
        {
            this._kernel = new StandardKernel(new DefaultRepositoriesNinjectModule());
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestAlbumListWithoutParameters()
        {
            using (var trackController = new TrackController(_kernel.Get<IRepositoryFactory>()))
            {
                trackController.AlbumList();
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestVieTestAlbumListWithinParameters()
        {
            using (var trackController = new TrackController(_kernel.Get<IRepositoryFactory>()))
            {
                trackController.AlbumList(1);
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestArtistList()
        {
            using (var trackController = new TrackController(_kernel.Get<IRepositoryFactory>()))
            {
                trackController.ArtistList();
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestArtistTracks()
        {
            using (var trackController = new TrackController(_kernel.Get<IRepositoryFactory>()))
            {
                trackController.ArtistTracks(1);
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestTrackDetails()
        {
            using (var trackController = new TrackController(_kernel.Get<IRepositoryFactory>()))
            {
                trackController.Details(1);
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestViewAllTracks()
        {
            using (var trackController = new TrackController(_kernel.Get<IRepositoryFactory>()))
            {
                trackController.TrackList();
            }
        }
    }
}