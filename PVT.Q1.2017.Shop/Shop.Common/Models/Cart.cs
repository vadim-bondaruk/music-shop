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
        public ICollection<Track> Tracks { get; set; }
    }
}
