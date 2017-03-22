namespace Shop.Common.Models.ViewModels
{
    using System;

    /// <summary>
    /// The track view model.
    /// </summary>
    public class TrackViewModel
    {
        /// <summary>
        /// Gets or sets the track id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the track name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the track release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the track image.
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
        /// Gets or sets the artist name.
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        public string GenreName { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        public TrackPrice Price { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        public double Rating { get; set; }
    }
}