namespace Shop.Common.ViewModels
{
    /// <summary>
    /// The track view model.
    /// </summary>
    public class TrackViewModel
    {
        /// <summary>
        /// Track id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Track name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Track rating.
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// The track artist.
        /// </summary>
        public ArtistViewModel Artist { get; set; }

        /// <summary>
        /// The track price.
        /// </summary>
        public PriceViewModel Price { get; set; }

        /// <summary>
        /// The track album id.
        /// </summary>
        public int AlbumId { get; set; }
    }
}