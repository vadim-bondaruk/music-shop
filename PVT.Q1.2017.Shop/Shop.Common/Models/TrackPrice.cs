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
    /// <summary>
    /// The track price.
    /// </summary>
    public class TrackPrice : BasePriceEntity
    {
        /// <summary>
        /// Track id
        /// </summary>
        public int TrackId { get; set; }

        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        public virtual Track Track { get; set; }
    }
}