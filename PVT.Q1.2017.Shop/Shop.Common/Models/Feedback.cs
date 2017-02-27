// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Feedback.cs" company="PVT Q1 2017">
//   All rights reserved
// </copyright>
// <summary>
//   Defines the Feedback type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    using Infrastructure.Models;

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
        public virtual Track Track { get; set; }

        /// <summary>
        /// Gets or sets the user who wrote the <see cref="Comments"/>.
        /// </summary>
        public virtual User User { get; set; }
    }
}