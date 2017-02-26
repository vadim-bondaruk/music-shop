namespace PVT.Q1._2017.Shop.ViewModels
{
    using global::Shop.Common.Models;
    using System.Collections.Generic;

    public class CartView
    {
        /// <summary>
        /// Gets or sets a Collection of tracks for View
        /// </summary>
        public IList<Track> Tracks { get; set; }
        /// <summary>
        /// Get total price of cart
        /// </summary>
        public decimal TotalPrice { get; }
    }
}