namespace Shop.Common.ViewModels
{
    using System;

    /// <summary>
    /// The album details view model.
    /// </summary>
    public class AlbumDetailsViewModel
    {
        /// <summary>
        /// Album id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Album name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Album cover.
        /// </summary>
        public byte[] Cover { get; set; }

        /// <summary>
        /// Album release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// The Artist. Optional.
        /// </summary>
        public ArtistViewModel Artist { get; set; }

        /// <summary>
        /// Album price.
        /// </summary>
        public PriceViewModel Price { get; set; }

        /// <summary>
        /// The number of the tracks from the album.
        /// </summary>
        public int TracksCount { get; set; }

        /// <summary>
        /// The album owner id.
        /// </summary>
        public int OwnerId { get; set; }
    }
}