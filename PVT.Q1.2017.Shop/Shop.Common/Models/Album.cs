// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Album.cs" company="PVT Q1 2017">
//   All rights reserved
// </copyright>
// <summary>
//   Defines the Album type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The album.
    /// </summary>
    public class Album : BaseNamedEntity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the cover.
        /// </summary>
        public byte[] Cover { get; set; }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        #endregion //Prperties

        #region Navigation Properties

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public virtual Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets all tracks from the album.
        /// </summary>
        public virtual ICollection<Track> Tracks { get; set; }

        /// <summary>
        /// Gets or sets the album prices.
        /// </summary>
        public virtual ICollection<AlbumPrice> AlbumPrices { get; set; }

        #endregion //Navigation Properties
    }
}