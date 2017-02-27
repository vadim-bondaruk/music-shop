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
    /// <summary>
    /// The album price.
    /// </summary>
    public class AlbumPrice : BasePriceEntity
    {
        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public virtual Album Album { get; set; }
    }
}