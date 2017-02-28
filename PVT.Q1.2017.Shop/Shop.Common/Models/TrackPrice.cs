namespace Shop.Common.Models
{
    /// <summary>
    /// The track price.
    /// </summary>
    public class TrackPrice : BasePriceEntity
    {
        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        public Track Track { get; set; }
    }
}