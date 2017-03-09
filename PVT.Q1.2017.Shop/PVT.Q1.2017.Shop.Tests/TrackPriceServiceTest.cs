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
    using global::Shop.BLL;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.DAL.Infrastruture;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ninject;

    /// <summary>
    /// </summary>
    [TestClass]
    public class TrackPriceServiceTest
    {
        /// <summary>
        /// </summary>
        private readonly ITrackPriceService _trackPriceService;

        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory _factory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TrackPriceServiceTest" /> class.
        /// </summary>
        public TrackPriceServiceTest()
        {
            IKernel kernel = new StandardKernel(new DefaultServicesNinjectModule());
            this._trackPriceService = kernel.Get<ITrackPriceService>();
            this._factory = kernel.Get<IRepositoryFactory>();

            var dbTest = new DataBaseTest();
            dbTest.RegisterValidTrackTest();

            var currencyTest = new CurrencyTest();
            currencyTest.AddValidCurrenciesTest();
        }
    }
}