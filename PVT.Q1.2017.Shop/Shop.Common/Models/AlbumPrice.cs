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
        /// Gets or sets the album cost amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }
    }
}