// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrackPrice.cs" company="PVT Q1 2017">
//   All rights reserved
// </copyright>
// <summary>
//   Defines the TrackPrice type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// The track price.
    /// </summary>
    public class TrackPrice : BaseEntity
    {
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        public Track Track { get; set; }
    }
}