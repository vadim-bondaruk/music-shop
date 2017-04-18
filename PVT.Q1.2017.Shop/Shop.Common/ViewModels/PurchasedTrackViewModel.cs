namespace Shop.Common.ViewModels
{
    using System;
    using System.ComponentModel;

    using Shop.Common.Models;

    /// <summary>
    /// The purchased track view model.
    /// </summary>
    public class PurchasedTrackViewModel
    {
        /// <summary>
        /// Track id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Track name.
        /// </summary>
        [DisplayName("Название")]
        public string Name { get; set; }

        /// <summary>
        /// Track rating.
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// Gets or sets the track release date.
        /// </summary>
        [DisplayName("Релиз")]
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the track duration.
        /// </summary>
        [DisplayName("Продолжительность")]
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// The track artist.
        /// </summary>
        [DisplayName("Исполнитель")]
        public ArtistViewModel Artist { get; set; }

        /// <summary>
        /// The track genre.
        /// </summary>
        [DisplayName("Жанр")]
        public Genre Genre { get; set; }

        /// <summary>
        /// The track media file.
        /// </summary>
        public byte[] TrackFile { get; set; }

        /// <summary>
        /// The track image. Optional.
        /// </summary>
        public byte[] Image { get; set; }
    }
}