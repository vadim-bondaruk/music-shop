// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Vote.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   The user vote.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    #region

    using Shop.Infrastructure.Models;

    #endregion

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
        ///     Gets or sets the track.
        /// </summary>
        public Track Track { get; set; }

        /// <summary>
        ///     Gets or sets the user.
        /// </summary>
        public User User { get; set; }
    }
}