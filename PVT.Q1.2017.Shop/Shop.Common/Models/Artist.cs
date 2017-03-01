namespace Shop.Common.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The artist.
    /// </summary>
    public class Artist : BaseNamedEntity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the biography.
        /// </summary>
        public string Biography { get; set; }

        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public byte[] Photo { get; set; }

        #endregion //Properties

        #region Navigation Properties

        /// <summary>
        /// Gets or sets all tracks of the current artist.
        /// </summary>
        public virtual ICollection<Track> Tracks { get; set; }

        /// <summary>
        /// Gets or sets the albums.
        /// </summary>
        public virtual ICollection<Album> Albums { get; set; }

        #endregion //Navigation Properties
    }
}