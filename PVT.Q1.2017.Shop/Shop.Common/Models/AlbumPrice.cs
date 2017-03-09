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
        #region Properties

        /// <summary>
        /// Price for track
        /// </summary>
        public decimal Price { get; set; }

        #endregion //Properties

        #region Foreign Keys

        /// <summary>
        /// Album id
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Get or set price level id
        /// </summary>
        public int PriceLevelId { get; set; }

        /// <summary>
        /// Currency id
        /// </summary>
        public int CurrencyId { get; set; }

        #endregion //Foreign Keys

        #region Navigation Properties

        /// <summary>
        /// Gets or sets the <see cref="Album"/>.
        /// </summary>
        public virtual Album Album { get; set; }

        /// <summary>
        /// Get or set <see cref="PriceLevel"/>
        /// </summary>
        public virtual PriceLevel PriceLevel { get; set; }

        /// <summary>
        /// The currency.
        /// </summary>
        public virtual Currency Currency { get; set; }

        #endregion //Navigation Properties
    }
}