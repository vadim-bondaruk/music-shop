// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrackPrice.cs" company="PVT Q1 2017">
//   All rights reserved
// </copyright>
// <summary>
//   Defines the TrackPrice type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// The track price.
    /// </summary>
    public class TrackPrice : BaseEntity
    {
        /// <summary>
        /// Currency id
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Track id
        /// </summary>
        public int TrackId { get; set; }

        /// <summary>
        /// Get or set price level id
        /// </summary>
        public int PriceLevelId { get; set; }

        /// <summary>
        /// Price for track
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        public virtual Track Track { get; set; }
        
        /// <summary>
        /// Get or set <see cref="PriceLevel"/>
        /// </summary>
        public virtual PriceLevel PriceLevel { get; set; }
    }
}