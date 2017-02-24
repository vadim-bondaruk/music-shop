// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Feedback.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   The feedback.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    #region

    using Shop.Infrastructure.Models;

    #endregion

    /// <summary>
    /// The feedback.
    /// </summary>
    public class Feedback : BaseEntity
    {
        /// <summary>
        /// Gets or sets the <see cref="User"/> comments.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the track for which the <see cref="User"/> have written the <see cref="Comments"/>.
        /// </summary>
        public Track Track { get; set; }

        /// <summary>
        /// Gets or sets the user who wrote the <see cref="Comments"/>.
        /// </summary>
        public User User { get; set; }
    }
}