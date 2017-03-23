namespace Shop.Common.Models.ViewModels
{
    using System;

    /// <summary>
    /// The track view model.
    /// </summary>
    public class TrackDetailsViewModel
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
        /// The track artist.
        /// </summary>
        public ArtistViewModel Artist { get; set; }

        /// <summary>
        /// The track genre.
        /// </summary>
        public GenreViewModel Genre { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        public PriceViewModel Price { get; set; }

        /// <summary>
        /// Track rating.
        /// </summary>
        public RatingViewModel Rating { get; set; }
    }
}