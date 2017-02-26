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
    using Infrastructure.Models;

    /// <summary>
    /// The album.
    /// </summary>
    public class Album : BaseEntity
    {
        /// <summary>
        /// Gets or sets the cover.
        /// </summary>
        public byte[] Cover { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the artist id.
        /// </summary>
        public int? ArtistId { get; set; }

        /// <summary>
        /// Gets or sets all tracks from the album.
        /// </summary>
        public ICollection<Track> Tracks { get; set; }

        /// <summary>
        /// Gets or sets the album prices.
        /// </summary>
        public ICollection<AlbumPrice> AlbumPrices { get; set; }
    }
}