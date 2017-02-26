// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlbumPrice.cs" company="PVT Q1 2017">
//   All rights reserved
// </copyright>
// <summary>
//   The album price.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// The album price.
    /// </summary>
    public class AlbumPrice : BaseEntity
    {
        /// <summary>
        /// Currency id
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Track id
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Get or set price level id
        /// </summary>
        public int PriceLevelId { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Album"/>.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Price level for this album
        /// </summary>
        public virtual PriceLevel PriceLevel { get; set; }
    }
}