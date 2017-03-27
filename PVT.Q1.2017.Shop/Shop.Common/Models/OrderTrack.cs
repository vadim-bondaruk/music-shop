namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// Tracks and Carts relation
    /// </summary>
    public class OrderTrack : BaseEntity
    {
        /// <summary>
        /// Cart ID
        /// </summary>
        public int CartId { get; set; }

        /// <summary>
        /// Track ID
        /// </summary>
        public int TrackId { get; set; }

        /// <summary>
        /// Track
        /// </summary>
        public virtual Track Track { get; set; }
    }
}
