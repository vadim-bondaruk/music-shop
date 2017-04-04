namespace Shop.Common.Models
{
    using System;
    using System.ComponentModel;

    /// <summary>
    ///     The track view model.
    /// </summary>
    public class TrackDetailsViewModel
    {
        /// <summary>
        ///     Gets or sets the album name.
        /// </summary>
        [DisplayName("Альбом")]
        public string AlbumName { get; set; }

        /// <summary>
        ///     The track artist.
        /// </summary>
        [DisplayName("Исполнитель")]
        public string ArtistName { get; set; }

        /// <summary>
        ///     Gets or sets the track duration.
        /// </summary>
        [DisplayName("Продолжительность")]
        public TimeSpan? Duration { get; set; }

        /// <summary>
        ///     Gets or sets the genre name.
        /// </summary>
        [DisplayName("Жанр")]
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
        [DisplayName("Название")]
        public string Name { get; set; }

        /// <summary>
        ///     The track genre.
        /// </summary>
        [DisplayName("Цена")]
        public PriceViewModel Price { get; set; }

        /// <summary>
        ///     Track rating.
        /// </summary>
        public RatingViewModel Rating { get; set; }

        /// <summary>
        ///     Gets or sets the track release date.
        /// </summary>
        [DisplayName("Дата выхода")]
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the track file.
        /// </summary>
        public byte[] TrackFile { get; set; }
    }
}