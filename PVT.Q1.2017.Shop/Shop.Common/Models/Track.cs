namespace Shop.Common.Models
{
    #region using

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The track.
    /// </summary>
    public class Track : BaseNamedEntity
    {
        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public virtual Album Album { get; set; }

        /// <summary>
        /// Gets or sets the album id.
        /// </summary>
        public int? AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public virtual Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the artist id.
        /// </summary>
        public int? ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the track duration.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Gets or sets all user feedbacks the current track.
        /// </summary>
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        public virtual Genre Genre { get; set; }

        /// <summary>
        /// Gets or sets the genre id.
        /// </summary>
        public int? GenreId { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        public string ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the track file.
        /// </summary>
        public byte[] TrackFile { get; set; }

        /// <summary>
        /// Gets or sets the track prices.
        /// </summary>
        public virtual ICollection<TrackPrice> TrackPrices { get; set; }

        /// <summary>
        /// Gets or sets all user votes for the current track.
        /// </summary>
        public virtual ICollection<Vote> Votes { get; set; }
    }
}