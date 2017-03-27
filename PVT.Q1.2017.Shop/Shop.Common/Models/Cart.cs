namespace Shop.Common.Models
{
    using System.Collections.Generic;
    using Infrastructure.Models;

    /// <summary>
    /// User's shoping cart
    /// </summary>
    public class Cart : BaseEntity
    {
        /// <summary>
        /// Current User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Collection of tracks for purchase
        /// </summary>
        public virtual ICollection<OrderTrack> Tracks { get; set; }

        /// <summary>
        /// Collection of albums for purchase
        /// </summary>
        public virtual ICollection<OrderAlbum> Albums { get; set; }
    }
}
