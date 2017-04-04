namespace Shop.Common.Models
{
    /// <summary>
    ///     The track view model.
    /// </summary>
    public class TrackViewModel
    {
        /// <summary>
        ///     The track artist.
        /// </summary>
        public ArtistViewModel Artist { get; set; }

        /// <summary>
        ///     Track id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        ///     Track name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The track price.
        /// </summary>
        public PriceViewModel Price { get; set; }

        /// <summary>
        ///     Track rating.
        /// </summary>
        public double Rating { get; set; }
    }
}