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
        /// Gets or sets the mark.
        /// </summary>
        public Mark Mark { get; set; }

        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        public Track Track { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public User User { get; set; }
    }
}