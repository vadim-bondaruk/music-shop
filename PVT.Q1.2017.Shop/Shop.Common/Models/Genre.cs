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
    using Infrastructure.Models;

    /// <summary>
    /// The genre.
    /// </summary>
    public class Genre : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}