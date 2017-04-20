namespace Shop.Common.Models
{
    using System.Collections.Generic;
    using Infrastructure.Models;

    /// <summary>
    /// User's shoping cart
    /// </summary>
    public class Cart : BaseEntity
    {
        public Cart()
        {
            OrderTracks = new List<OrderTrack>();
            OrderAlbums = new List<OrderAlbum>();
        }

        public Cart(int userId)
        {
            UserId = userId;
            OrderTracks = new List<OrderTrack>();
            OrderAlbums = new List<OrderAlbum>();
        }
        /// <summary>
        /// Current User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Collection of tracks for purchase
        /// </summary>
        public virtual ICollection<OrderTrack> OrderTracks { get; set; }

        /// <summary>
        /// Collection of albums for purchase
        /// </summary>
        public virtual ICollection<OrderAlbum> OrderAlbums { get; set; }
    }
}
