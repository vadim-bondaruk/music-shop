using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PVT.Q1._2017.Shop.ViewModels;
using Shop.Common.Models;

namespace PVT.Q1._2017.Shop.Tests
{
    [TestClass]
    public class CartViewModelTests
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
            myCartView.SetTotalPrice(userCurrency);
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
            myCartView.SetTotalPrice(userCurrency);
            Assert.IsTrue(myCartView.TotalPrice == 240);
        }
    }
}
