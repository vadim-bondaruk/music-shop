// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="Genre.cs" company="PVT Q1 2017">
// //   All rights reserved
// // </copyright>
// // <summary>
// //   Defines the Genre type.
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The genre.
    /// </summary>
    public class Genre : BaseNamedEntity
    {
        #region Navigation Properties

        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        public ICollection<Track> Tracks { get; set; }

        #endregion //Navigation Properties
    }
}