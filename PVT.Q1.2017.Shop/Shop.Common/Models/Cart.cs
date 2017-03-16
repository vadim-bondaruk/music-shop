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
        /// Gets or Sets a Current User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets a Collection of tracks for purchase
        /// </summary>
        public ICollection<Track> Tracks { get; set; }
    }
}
