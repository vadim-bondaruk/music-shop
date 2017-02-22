// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Artist.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Artist type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    using System;
    using System.Collections.Generic;

    using Shop.Infrastructure.Models;

    /// <summary>
    /// The artist.
    /// </summary>
    public class Artist : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// Gets or sets the biography.
        /// </summary>
        public string Biography { get; set; }

        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        public virtual IEnumerable<Track> Tracks { get; set; }

        /// <summary>
        /// Gets or sets the albums.
        /// </summary>
        public virtual IEnumerable<Album> Albums { get; set; }
    }
}