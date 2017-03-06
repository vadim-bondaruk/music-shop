namespace PVT.Q1._2017.Shop.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PVT.Q1._2017.Shop.Controllers;

    /// <summary>
    /// </summary>
    [TestClass]
    public class TrackControllerTests
    {
        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestAlbumListWithoutParameters()
        {
            using (var trackController = new TrackController())
            {
                trackController.AlbumList();
            }
        }        
        
        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestVieTestAlbumListWithinParameters()
        {
            using (var trackController = new TrackController())
            {
                trackController.AlbumList(1);
            }
        }        
        
        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestArtistList()
        {
            using (var trackController = new TrackController())
            {
                trackController.ArtistList();
            }
        }        
        
        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestArtistTracks()
        {
            using (var trackController = new TrackController())
            {
                trackController.ArtistTracks(1);
            }
        }        
        
        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestTrackDetails()
        {
            using (var trackController = new TrackController())
            {
                trackController.Details(1);
            }
        }       
        
        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestViewAllTracks()
        {
            using (var trackController = new TrackController())
            {
                trackController.TrackList();
            }
        }       
    }
}