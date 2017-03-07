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
        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the cover.
        /// </summary>
        public byte[] Cover { get; set; }

        /// <summary>
        /// Gets or sets the album release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        #endregion //Prperties

        #region Foreign Keys

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public int? ArtistId
        {
            get; set;
        }

        #endregion //Foreign Keys

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