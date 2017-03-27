namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// Albums and Carts relation
    /// </summary>
    public class OrderAlbum : BaseEntity
    {
        /// <summary>
        /// Cart ID
        /// </summary>
        public int CartId { get; set; }

        /// <summary>
        /// Album ID
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Album
        /// </summary>
        public virtual Album Album { get; set; }
    }
}
