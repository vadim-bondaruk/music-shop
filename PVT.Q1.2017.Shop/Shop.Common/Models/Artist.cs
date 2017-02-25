﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Artist.cs" company="PVT Q1 2017">
//   All rights reserved
// </copyright>
// <summary>
//   Defines the Artist type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Infrastructure.Models;

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
        /// Gets or sets the biography.
        /// </summary>
        public string Biography { get; set; }

        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// Gets or sets all tracks of the current artist.
        /// </summary>
        public ICollection<Track> Tracks { get; set; }

        /// <summary>
        /// Gets or sets the albums.
        /// </summary>
        public ICollection<Album> Albums { get; set; }
    }
}