using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Shop.BLL;
using Shop.BLL.Services;

namespace PVT.Q1._2017.Shop.Tests
{
    /// <summary>
    /// Summary description for TrackServiceTest
    /// </summary>
    [TestClass]
    public class TrackServiceTest
    {
        #region Fields

        private IKernel _kernel;

        #endregion //Fields

        public TrackServiceTest()
        {
            this._kernel = new StandardKernel(new DefaultServicesNinjectModule());
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TracksWithArtistsTest()
        {
            List<string> artists = new List<string>();

            ITrackService trackService = this._kernel.Get<ITrackService>();
            var tracks = trackService.GetAllRegisteredTracks();

            foreach (var track in tracks)
            {
                if (track.ArtistId != null)
                {
                    artists.Add(track.Artist.Name);
                }
            }

            Assert.IsTrue(artists.Count > 0);
        }
    }
}
