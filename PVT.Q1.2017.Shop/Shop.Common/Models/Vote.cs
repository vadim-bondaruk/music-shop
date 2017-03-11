// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Vote.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    ///     The user vote.
    /// </summary>
    public class Vote : BaseEntity
    {
        /// <summary>
        ///     Gets or sets the mark.
        /// </summary>
        public Mark Mark { get; set; }

        /// <summary>
        ///     Gets or sets the track id.
        /// </summary>
        public int TrackId { get; set; }

        /// <summary>
        ///     Gets or sets the user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Gets or sets the track.
        /// </summary>
        public virtual Track Track { get; set; }

        /// <summary>
        ///     Gets or sets the user.
        /// </summary>
        public virtual User User { get; set; }
    }
}