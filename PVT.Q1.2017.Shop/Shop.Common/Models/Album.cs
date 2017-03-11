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
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the cover.
        /// </summary>
        public byte[] Cover { get; set; }

        /// <summary>
        /// Gets or sets the album release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public int? ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public virtual Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets all tracks from the album.
        /// </summary>
        public virtual ICollection<AlbumTrackRelation> Tracks { get; set; }

        /// <summary>
        /// Gets or sets the album prices.
        /// </summary>
        public virtual ICollection<AlbumPrice> AlbumPrices { get; set; }
    }
}