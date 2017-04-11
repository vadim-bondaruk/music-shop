namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// Purchased albums by User
    /// </summary>
    public class PurchasedAlbum : BaseEntity
    {
        /// <summary>
        /// User ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Purchased album ID
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public virtual UserData User { get; set; }

        /// <summary>
        /// Purchased Album
        /// </summary>
        public virtual Album Album { get; set; }
    }
}
