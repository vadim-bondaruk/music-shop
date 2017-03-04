namespace PVT.Q1._2017.Shop.Tests
{
    using System.Linq;
    using global::Shop.BLL;
    using global::Shop.BLL.Services;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Repositories.Infrastruture;
    using global::Shop.Infrastructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;

    [TestClass]
    public class TrackPriceServiceTest
    {
        private readonly ITrackPriceService _trackPriceService;
        private readonly IFactory _factory;

        public TrackPriceServiceTest()
        {
            IKernel kernel = new StandardKernel(new DefaultServicesNinjectModule());
            this._trackPriceService = kernel.Get<ITrackPriceService>();
            this._factory = kernel.Get<IFactory>();

            TrackServiceTest trackServiceTest = new TrackServiceTest();
            trackServiceTest.RegisterValidTrackTest();

            CurrencyTest currencyTest = new CurrencyTest();
            currencyTest.AddValidCurrenciesTest();
        }

        [TestMethod]
        public void AddPriceTest()
        {
            Track track;
            using (var repository = this._factory.Create<ITrackRepository>())
            {
                track = repository.GetAll(t => t.ArtistId.HasValue && t.AlbumId.HasValue).FirstOrDefault();
            }
            Assert.IsNotNull(track);

            // this._trackPriceService.AddTrackPrice(track);
        }
    }
}