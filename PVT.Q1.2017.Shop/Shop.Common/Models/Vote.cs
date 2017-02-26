// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Vote.cs" company="PVT Q1 2017">
//   All rights reserved
// </copyright>
// <summary>
//   The user vote.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// The user vote.
    /// </summary>
    public class Vote : BaseEntity
    {
        /// <summary>
        /// Mark id
        /// </summary>
        public int MarkId { get; set; }

        /// <summary>
        /// Track id
        /// </summary>
        public int TrackId { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the mark.
        /// </summary>
        public Mark Mark { get; set; }

        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        public virtual Track Track { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual User User { get; set; }
    }
}