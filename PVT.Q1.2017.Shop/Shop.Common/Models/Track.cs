namespace Shop.Common.Models
{
    using System;
    using System.Collections.Generic;
    using FluentValidation.Attributes;
    using Infrastructure.Models;
    using Validators;

    /// <summary>
    /// The track.
    /// </summary>
    [Validator(typeof(TrackValidator))]
    public class Track : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the track release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Gets or sets the track file.
        /// </summary>
        public byte[] TrackFile { get; set; }

        /// <summary>
        /// Gets or sets the track duration.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Gets or sets the artist id.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the genre id.
        /// </summary>
        public int GenreId { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public virtual Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        public virtual Genre Genre { get; set; }

        /// <summary>
        /// Gets or sets the albums which contain the current track.
        /// </summary>
        public virtual ICollection<AlbumTrackRelation> Albums { get; set; }

        /// <summary>
        /// Gets or sets the track prices.
        /// </summary>
        public virtual ICollection<TrackPrice> TrackPrices { get; set; }

        /// <summary>
        /// Gets or sets all user votes for the current track.
        /// </summary>
        public virtual ICollection<Vote> Votes { get; set; }

        /// <summary>
        /// Gets or sets all user feedbacks the current track.
        /// </summary>
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}