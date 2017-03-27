namespace Shop.Common.ViewModels
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// CartView model
    /// </summary>
    public class CartViewModel
    {
        /// <summary>
        /// Gets or sets a Collection of tracks for View
        /// </summary>
        public int CurrentUserId { get; set; }

        /// <summary>
        /// Gets or sets a Collection of tracks for View
        /// </summary>
        public ICollection<Track> Tracks { get; set; }

        /// <summary>
        /// Gets or sets a Collection of Albums for View
        /// </summary>
        public ICollection<Album> Albums { get; set; }

        /// <summary>
        /// Get total price of cart
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Get currency short name
        /// </summary>
        public string CurrencyShortName { get; set; }
    }
}
