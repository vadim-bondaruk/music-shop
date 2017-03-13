namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// The albums and tracks relations
    /// </summary>
    public class AlbumTrackRelation : BaseEntity
    {
        /// <summary>
        /// Gets or sets the album id.
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public virtual Album Album { get; set; }

        /// <summary>
        /// Gets or sets the track id.
        /// </summary>
        public int TrackId { get; set; }

        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        public virtual Track Track { get; set; }
    }
}