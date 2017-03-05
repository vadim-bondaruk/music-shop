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
    #region

    using Shop.Infrastructure.Models;

    #endregion

    /// <summary>
    ///     The user vote.
    /// </summary>
    public class Vote : BaseEntity
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the mark.
        /// </summary>
        public Mark Mark { get; set; }

        #endregion //Properties

        #region Foreign Keys

        /// <summary>
        ///     Gets or sets the track id.
        /// </summary>
        public int TrackId { get; set; }

        /// <summary>
        ///     Gets or sets the user id.
        /// </summary>
        public int UserId { get; set; }

        #endregion //Foreign Keys

        #region Navigation Properties

        /// <summary>
        ///     Gets or sets the track.
        /// </summary>
        public virtual Track Track { get; set; }

        /// <summary>
        ///     Gets or sets the user.
        /// </summary>
        public virtual User User { get; set; }

        #endregion //Navigation Properties
    }
}