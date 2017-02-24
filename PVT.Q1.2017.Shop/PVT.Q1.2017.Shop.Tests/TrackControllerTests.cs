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
        public void TestViewAllTracks()
        {
            using (var trackController = new TrackController())
            {
                trackController.TrackList();
            }
        }
    }
}