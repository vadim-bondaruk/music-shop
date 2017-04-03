namespace Shop.Common.Models.ViewModels
{
    using System;

    /// <summary>
    ///     The track view model.
    /// </summary>
    public class TrackDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the album name.
        /// </summary>
        public string AlbumName { get; set; }

        /// <summary>
        ///     The track artist.
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        ///     Gets or sets the track duration.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        ///     Gets or sets the genre name.
        /// </summary>
        public string GenreName { get; set; }

        /// <summary>
        ///     Gets or sets the track id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the track image.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        ///     Gets or sets the track name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The track genre.
        /// </summary>
        public PriceViewModel Price { get; set; }

        /// <summary>
        ///     Track rating.
        /// </summary>
        public RatingViewModel Rating { get; set; }

        /// <summary>
        ///     Gets or sets the track release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        ///     Gets or sets the track file.
        /// </summary>
        public byte[] TrackFile { get; set; }
    }
}