// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrackPrice.cs" company="PVT Q1 2017">
//   All rights reserved
// </copyright>
// <summary>
//   CartView Model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PVT.Q1._2017.Shop.ViewModels
{
    using System.Collections.Generic;
    using global::Shop.Common.Models;

    /// <summary>
    /// CartView model
    /// </summary>
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