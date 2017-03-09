//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Track.cs" company="PVT.Q1.2017">
//    PVT.Q1.2017
//  </copyright>
//  <summary>
//    The track.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace PVT.Q1._2017.Shop.Tests
{
    using System.Linq;

    using global::Shop.BLL;
    using global::Shop.BLL.Services.Infrastructure;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ninject;

    /// <summary>
    ///     Summary description for TrackServiceTest
    /// </summary>
    [TestClass]
    public class TrackServiceTest
    {
        #region Fields

        /// <summary>
        /// </summary>
        private readonly IKernel _kernel;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackServiceTest"/> class.
        /// </summary>
        public TrackServiceTest()
        {
            this._kernel = new StandardKernel(new DefaultServicesNinjectModule());

            var dbTest = new DataBaseTest();
            dbTest.RegisterValidTrackTest();
        }

        #endregion //Constructors

        #region Tests

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TracksListTest()
        {
            var trackService = this.GetTrackService();
            Assert.IsTrue(trackService.GetTracksList().Any());
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TrackInfoTest()
        {
            var trackService = this.GetTrackService();

            var track = trackService.GetTracksList().FirstOrDefault(t => t.ArtistId.HasValue && t.AlbumId.HasValue);
            Assert.IsNotNull(track);

            track = trackService.GetTrackInfo(track.Id);
            Assert.IsNotNull(track);
            Assert.IsTrue(track.Artist != null);
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TrackPricesTest()
        {
            var trackService = this.GetTrackService();
            Assert.IsTrue(trackService.GetTracksWithoutPriceConfigured().Any());
        }

        #endregion //Tests

        #region Private Methods

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        private ITrackService GetTrackService()
        {
            return this._kernel.Get<ITrackService>();
        }

        #endregion //Private Methods
    }
}