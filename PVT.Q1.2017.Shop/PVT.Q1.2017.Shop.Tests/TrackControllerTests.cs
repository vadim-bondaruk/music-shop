namespace PVT.Q1._2017.Shop.Tests
{
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.DAL;
    using global::Shop.DAL.Infrastruture;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;
    using PVT.Q1._2017.Shop.Controllers;

    /// <summary>
    /// </summary>
    [TestClass]
    public class TrackControllerTests
    {
        #region Fields

        private readonly IRepositoryFactory _factory;
        private readonly ITrackService _trackService;

        #endregion //Fields

        #region Constructors

        public TrackControllerTests()
        {
            IKernel kernel = new StandardKernel(new DefaultRepositoriesNinjectModule());
            this._factory = kernel.Get<IRepositoryFactory>();
        }

        #endregion //Constructors

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestAlbumListWithoutParameters()
        {
            //var trackController = new TrackController(this._factory, this._trackService);
            //trackController.AlbumList();
        }        
        
        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestVieTestAlbumListWithinParameters()
        {
            //using (var trackController = new TrackController())
            //{
            //    trackController.AlbumList(1);
            //}
        }        
        
        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestArtistList()
        {
            //using (var trackController = new TrackController())
            //{
            //    trackController.ArtistList();
            //}
        }        
        
        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestArtistTracks()
        {
            //using (var trackController = new TrackController())
            //{
            //    trackController.ArtistTracks(1);
            //}
        }        
        
        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestTrackDetails()
        {
            //using (var trackController = new TrackController())
            //{
            //    trackController.Details(1);
            //}
        }       
        
        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestViewAllTracks()
        {
            //using (var trackController = new TrackController())
            //{
            //    trackController.TrackList();
            //}
        }       
    }
}