// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlbumPrice.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   The album price.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    /// <summary>
    ///     The album price.
    /// </summary>
    public class AlbumPrice : BasePriceEntity
    {
        /// <summary>
        /// Track id
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Album"/>.
        /// </summary>
        public Album Album { get; set; }
    }
}