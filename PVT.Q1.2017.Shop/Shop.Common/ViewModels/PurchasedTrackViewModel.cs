namespace Shop.Common.ViewModels
{
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
        public byte[] TrackFile { get; set; }
    }
}