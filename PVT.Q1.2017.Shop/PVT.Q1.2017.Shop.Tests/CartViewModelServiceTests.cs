using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.BLL.Services;
using Shop.Common.Models;
using Shop.Common.Models.ViewModels;
using System.Collections.Generic;

namespace PVT.Q1._2017.Shop.Tests
{
    [TestClass]
    public class CartViewModelServiceTests
    {
        [TestMethod]
        public void SetTotalPrice_noTracksInCart_0()
        {
            //// Set Currency
            var userCurrency = new Currency();
            userCurrency.Code = 840;
            userCurrency.ShortName = "USD";
            //// Create and set CartView model
            var myCartView = new CartViewModel() { Tracks = new List<Track>() };
            CartViewModelService.SetTotalPrice(myCartView, userCurrency);
            Assert.IsTrue(myCartView.TotalPrice == 0);
        }

        [TestMethod]
        public void SetTotalPrice_120and120_240()
        {
            //// Set Currency
            var userCurrency = new Currency();
            userCurrency.Code = 840;
            userCurrency.ShortName = "USD";
            //// Set TrackPrices
            var priceTrack = new TrackPrice();
            priceTrack.Currency = userCurrency;
            priceTrack.Price = 120;
            var pricesTrack = new List<TrackPrice>();
            pricesTrack.Add(priceTrack);
            //// Create two tracks
            var track1 = new Track()
            {
                Name = "SuperTrack",
                TrackPrices = pricesTrack
            };
            var track2 = new Track()
            {
                Name = "SuperTrack",
                TrackPrices = pricesTrack
            };
            //// Create and set CartView model
            var myCartView = new CartViewModel() { Tracks = new List<Track>() };
            myCartView.Tracks.Add(track1);
            myCartView.Tracks.Add(track2);
            CartViewModelService.SetTotalPrice(myCartView, userCurrency);
            Assert.IsTrue(myCartView.TotalPrice == 240);
        }
    }
}
