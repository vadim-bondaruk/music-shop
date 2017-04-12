namespace Shop.Common.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Shop.Common.Models;

    /// <summary>
    ///     The album details view model.
    /// </summary>
    public class AlbumDetailsViewModel
    {
        /// <summary>
        ///     The Artist. Optional.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        ///     Gets or sets the artist name.
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        ///     Album cover.
        /// </summary>
        public byte[] Cover { get; set; }

        /// <summary>
        ///     Album id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the album is ordered.
        /// </summary>
        public bool IsOrdered { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the album is purchased.
        /// </summary>
        public bool IsPurchased { get; set; }

        /// <summary>
        ///     Album name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The album owner id.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        ///     Album price.
        /// </summary>
        public PriceViewModel Price { get; set; }

        /// <summary>
        ///     Album release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        public ICollection<Track> Tracks { get; set; }

        /// <summary>
        ///     The number of the tracks from the album.
        /// </summary>
        public int TracksCount { get; set; }
    }
}