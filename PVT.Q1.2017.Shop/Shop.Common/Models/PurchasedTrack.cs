﻿namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// Purchased tracks by User
    /// </summary>
    public class PurchasedTrack : BaseEntity
    {
        /// <summary>
        /// User ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Purchased track ID
        /// </summary>
        public int TrackId { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public virtual UserData User { get; set; }

        /// <summary>
        /// Purchased Track
        /// </summary>
        public virtual Track Track { get; set; }
    }
}
